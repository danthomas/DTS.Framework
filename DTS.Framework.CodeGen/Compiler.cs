using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DTS.Framework.DomainDefinition;

namespace DTS.Framework.CodeGen
{
    public class CompileResult
    {
        public bool HasErrors { get; set; }

        public ITemplate template { get; set; }

        public List<string> Errors { get; set; }
    }

    public class Compiler
    {
        public CompileResult Compile(BuildResult buildResult)
        {
            CompileResult compileResult = new CompileResult();

            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("C#");
            CompilerParameters compilerParameters = new CompilerParameters { GenerateInMemory = true };
            compilerParameters.ReferencedAssemblies.Add("System.dll");
            compilerParameters.ReferencedAssemblies.Add("DTS.Framework.DomainDefinition.dll");
            compilerParameters.ReferencedAssemblies.Add("DTS.Framework.CodeGen.dll");

            compilerParameters.IncludeDebugInformation = false;

            CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromSource(compilerParameters, buildResult.Code);

            if (compilerResults.Errors.HasErrors)
            {
                compileResult.HasErrors = true;
                compileResult.Errors = compilerResults.Errors.Cast<CompilerError>().Select(item => item.ErrorText).ToList();
            }
            else
            {
                Assembly compiledAssembly = compilerResults.CompiledAssembly;
                compileResult.template = (ITemplate)compiledAssembly.CreateInstance(buildResult.FullTypeName);
            }

            return compileResult;
        }
    }
}

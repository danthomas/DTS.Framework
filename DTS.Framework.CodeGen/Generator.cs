using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using DTS.Framework.DomainDefinition;

namespace DTS.Framework.CodeGen
{
    public class Generator
    {
        private readonly Type _type;
        private ITemplate _template;

        public Generator(Type type)
        {
            _type = type;

            TemplateFilePath = FindTemplateFilePath();

            TemplateText = File.ReadAllText(TemplateFilePath);

            CompileTemplate();
        }

        public string TemplateFilePath { get; private set; }

        private string TemplateText { get; set; }

        private string FindTemplateFilePath()
        {
            string directoryPath = Path.GetDirectoryName((new Uri(_type.Assembly.CodeBase)).AbsolutePath);

            directoryPath = directoryPath.Replace("\\bin\\Debug", "");

            directoryPath = Directory.GetParent(directoryPath).FullName;
            
            List<string> filePaths = Directory.GetFiles(directoryPath, "*.txt", SearchOption.AllDirectories).Where(item => item.EndsWith(String.Format("\\{0}.txt", _type.Name))).ToList();

            if (filePaths.Count != 1)
            {
                throw new DomainDefinitionException(DomainDefinitionExceptionType.TemplateFileNotFound, "Failed to find Template file for {0}", _type.Name);
            }

            return filePaths[0];
        }

        public List<GenFile> Transform(Domain domain)
        {
            List<GenFile> genFiles = new List<GenFile>();

            if (_template is DomainTemplate)
            {
                DomainTemplate domainTemplate = (DomainTemplate)_template;
                domainTemplate.Domain = domain;
                genFiles.Add(new GenFile(_template.RelativeFilePath, (string)_template.GetType().GetMethod("TransformText").Invoke(_template, new object[0])));
            }
            else if (_template is EntityTemplate)
            {
                foreach (Entity entity in domain.Groups.SelectMany(item => item.Entities))
                {
                    EntityTemplate entityTemplate = (EntityTemplate)_template;
                    entityTemplate.Entity = entity;
                    genFiles.Add(new GenFile(_template.RelativeFilePath, (string)_template.GetType().GetMethod("TransformText").Invoke(_template, new object[0])));
                }
            }

            return genFiles;
        }

        private void CompileTemplate()
        {
            string[] parts = Regex.Split(TemplateText, @"(<#|#>)");

            bool isCode = false;
            string code = String.Format(@"using System;
using System.Text;
using DTS.Framework.CodeGen;

class Template : {0}
{{
    public override string TransformText()
    {{
        StringBuilder stringBuilder = new StringBuilder();
", _type.FullName);

            foreach (string part in parts)
            {
                if (part == "<#")
                {
                    isCode = true;
                }
                else if (part == "#>")
                {
                    isCode = false;
                }
                else if (isCode)
                {
                    if (part.StartsWith("="))
                    {
                        code += @"

        stringBuilder.Append(" + part.Substring(1).Trim() + ");";
                    }
                    else
                    {
                        code += part;
                    }
                }
                else
                {
                    if (part != "")
                    {
                        string text = part.Replace(@"""", @"""""");

                        code += @"

        stringBuilder.Append(@""" + text + @""");";
                    }
                }
            }

            code += @"

        return stringBuilder.ToString();
    }
}";
            File.WriteAllText(@"C:\Users\Dan\Source\Repos\DTS.Framework\DTS.Framework.Tests\DTS.Framework.CodeGen\Template.cs", code);

            _template = (ITemplate) Compile(code);
        }

        private object Compile(string code)
        {
            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("C#");
            CompilerParameters compilerParameters = new CompilerParameters { GenerateInMemory = true };
            compilerParameters.ReferencedAssemblies.Add("System.dll");
            compilerParameters.ReferencedAssemblies.Add("DTS.Framework.DomainDefinition.dll");
            compilerParameters.ReferencedAssemblies.Add("DTS.Framework.CodeGen.dll");

            compilerParameters.IncludeDebugInformation = false;

            CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromSource(compilerParameters, code);

            if (compilerResults.Errors.HasErrors)
            {
                throw new DomainDefinitionException(DomainDefinitionExceptionType.TemplateCompileFailed, "Failed to compile template.");
            }

            Assembly compiledAssembly = compilerResults.CompiledAssembly;


            return compiledAssembly.CreateInstance("Template");
        }
    }
}

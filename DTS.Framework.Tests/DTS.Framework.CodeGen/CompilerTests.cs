using DTS.Framework.CodeGen;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.Framework.Tests.DTS.Framework.CodeGen
{
    [TestClass]
    public class CompilerTests
    {
        [TestMethod]
        public void Compile()
        {
            Builder builder = new Builder();

            foreach (BuildResult buildResult in builder.Build())
            {
                Compiler compiler = new Compiler();

                CompileResult compileResult = compiler.Compile(buildResult);
            }
        }
    }
}

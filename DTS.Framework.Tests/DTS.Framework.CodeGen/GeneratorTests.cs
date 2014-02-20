using System.IO;
using DTS.Framework.CodeGen;
using DTS.Framework.DomainDefinition;
using DTS.Framework.Tests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.Framework.Tests.DTS.Framework.CodeGen
{
    [TestClass]
    public class GeneratorTests
    {
        [TestMethod]
        public void Generate()
        {
            string directoryPath = Directory.GetCurrentDirectory();

            directoryPath = directoryPath.Replace(@"DTS.Framework.Tests\bin\Debug", "");

            Domain domain = Domains.CreateManagerDomain();

            Generator generator = new Generator();

            foreach (GenerateResult genFile in generator.Generate(domain))
            {
                string filePath = Path.Combine(directoryPath, genFile.RelativeFilePath);

                if (!File.Exists(filePath) || genFile.Text != File.ReadAllText(filePath))
                {
                    File.WriteAllText(filePath, genFile.Text);
                }
            }
        }
    }
}

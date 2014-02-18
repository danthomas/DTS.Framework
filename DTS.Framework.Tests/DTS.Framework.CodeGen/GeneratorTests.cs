using System;
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
        public void GenerateManager()
        {
            string directoryPath = Directory.GetCurrentDirectory();

            directoryPath = directoryPath.Replace(@"DTS.Framework.Tests\bin\Debug", "");

            Domain domain = Domains.CreateManagerDomain();

            Generator generator = new Generator();

            generator.Generate(directoryPath, domain);
        }

        [TestMethod]
        public void GenerateMusic()
        {
            string directoryPath = Directory.GetCurrentDirectory();

            directoryPath = directoryPath.Replace(@"DTS.Framework.Tests\bin\Debug", "");

            Domain domain = Domains.CreateMusicDomain();

            Generator generator = new Generator();

            generator.Generate(directoryPath, domain);
        }
    }
}

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
        public void Generate()
        {
            string directoryPath = Directory.GetCurrentDirectory();

            directoryPath = directoryPath.Replace(@"DTS.Framework.Tests\bin\Debug", "");

            Domain domain = Music.CreateMusicDomain();

            Generator generator = new Generator();

            generator.Generate(directoryPath, domain);
        }
    }
}

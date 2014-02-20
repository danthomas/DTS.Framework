using System;
using System.Collections.Generic;
using System.IO;
using DTS.Framework.CodeGen;
using DTS.Framework.Tests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.Framework.Tests.DTS.Framework.CodeGen
{
    [TestClass]
    public class GeneratorTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            string directoryPath = Directory.GetCurrentDirectory();

            directoryPath = directoryPath.Replace(@"DTS.Framework.Tests\bin\Debug", "");

            //Generate(directoryPath, typeof(Class));
            //Generate(directoryPath, typeof(Schemas));
            Generate(directoryPath, typeof(Table));
        }

        private static void Generate(string directoryPath, Type type)
        {
            Generator generator = new Generator(type);

            List<GenFile> genFiles = generator.Transform(Domains.CreateManagerDomain());

            foreach (GenFile genFile in genFiles)
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

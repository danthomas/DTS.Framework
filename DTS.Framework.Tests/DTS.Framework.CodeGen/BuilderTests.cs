using System.Collections.Generic;
using System.IO;
using DTS.Framework.CodeGen;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.Framework.Tests.DTS.Framework.CodeGen
{
    [TestClass]
    public class BuilderTests
    {
        [TestMethod]
        public void Build()
        {
            Builder builder = new Builder();

            List<BuildResult> buildResults = builder.Build();

            foreach (BuildResult buildResult in buildResults)
            {
                if (buildResult.Code != File.ReadAllText(buildResult.GenFilePath))
                {
                    File.WriteAllText(buildResult.GenFilePath, buildResult.Code);
                }
            }
        }
    }
}

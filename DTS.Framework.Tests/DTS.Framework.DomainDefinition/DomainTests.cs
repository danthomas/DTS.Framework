using System;
using System.Security.Cryptography.X509Certificates;
using DTS.Framework.DomainDefinition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.Framework.Tests.DTS.Framework.DomainDefinition
{
    [TestClass]
    public class DomainTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Domain domain = new Domain("Music", new DomainOptions
            {
                AutoIdProperty = true
            });

            domain.AddDefaultDataTypes();

            Group group = domain.AddGroup("Main");
            
            Entity artistEntity = group.AddEntity<short>("Artist")
                .AddProperty<string>("Name");

            Entity albumEntity = group.AddEntity<short>("Album")
                .AddProperty<string>("Name")
                .AddProperty(artistEntity);

            Entity trackEntity = group.AddEntity("Track")
                .AddProperty<string>("Name")
                .AddProperty<TimeSpan>("Length")
                .AddProperty(artistEntity)
                .AddProperty(albumEntity);

            Entity genreEntity = group.AddEntity<short>("Genre")
                .AddProperty<string>("Name");

            Entity playListEntity = group.AddEntity<short>("PlayList")
                .AddProperty<string>("Name")
                .AddMany(trackEntity);


            string actual = domain.ToString();

            Assert.AreEqual(@"", actual);
        }
    }
}

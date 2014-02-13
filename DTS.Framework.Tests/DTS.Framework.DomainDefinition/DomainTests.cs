using System;
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
            Domain domain = new Domain("Music");

            Group group = domain.AddGroup("Main");
            
            Entity artistEntity = group.AddEntity("Artist")
                .AddProperty("ArtistId", typeof(short))
                .SetIdentifier("ArtistId");

            Entity albumEntity = group.AddEntity("Album");

            Entity trackEntity = group.AddEntity("Track");

            Entity genreEntity = group.AddEntity("Genre");

            Entity playListEntity = group.AddEntity("PlayList");




        }
    }
}

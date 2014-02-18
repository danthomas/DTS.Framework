using System;
using DTS.Framework.DomainDefinition;
using DTS.Framework.Tests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.Framework.Tests.DTS.Framework.DomainDefinition
{
    [TestClass]
    public class DomainTests
    {
        [TestMethod]
        public void Domain_ToString()
        {
            Domain domain = Domains.CreateMusicDomain();

            string actual = domain.ToString();

            Assert.AreEqual(@"<Music
  Main
    Genre
      GenreId SmallInt
      Name String
    Artist
      ArtistId SmallInt
      Name String
    Album
      AlbumId SmallInt
      Name String
      Artist Artist
      Genre Genre
    Track
      TrackId Int
      Name String
      Length TimeSpan
      Artist Artist
      Album Album
      Genre Genre
    Playlist
      PlaylistId SmallInt
      Name String
    Playlist
      PlaylistId Int
      Playlist Playlist
      Track Track
      Order TinyInt", actual);
        }
    }
}

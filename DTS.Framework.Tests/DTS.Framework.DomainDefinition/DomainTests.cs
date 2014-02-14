using System;
using DTS.Framework.DomainDefinition;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DTS.Framework.Tests.DTS.Framework.DomainDefinition
{
    [TestClass]
    public class DomainTests
    {
        [TestMethod]
        public void CreateMusicDomain()
        {
            Domain domain = new Domain("Music", new DomainOptions
            {
                AutoIdProperty = true,
                AutoIdPropertyType = typeof(Int16)
            }).AddDefaultDataTypes();

            Group group = domain.AddGroup("Main");

            Entity genre = group.AddEntity("Genre")
                .Value<string>("Name");
            
            Entity artist = group.AddEntity("Artist")
                .Value<string>("Name");

            Entity album = group.AddEntity("Album")
                .Value<string>("Name")
                .Reference(artist)
                .Reference(genre, true);

            Entity track = group.AddEntity<int>("Track")
                .Value<string>("Name")
                .Value<TimeSpan>("Length")
                .Reference(artist)
                .Reference(album)
                .Reference(genre, true);

            Entity playlist = group.AddEntity("PlayList")
                .Value<string>("Name");

            Entity playlistTrack = group.AddEntity<int>("PlayList")
                .Reference(playlist)
                .Reference(track)
                .Value<byte>("Order");

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
    PlayList
      PlayListId SmallInt
      Name String
    PlayList
      PlayListId Int
      PlayList PlayList
      Track Track
      Order TinyInt", actual);
        }
    }
}

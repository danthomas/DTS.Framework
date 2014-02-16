using System;
using DTS.Framework.DomainDefinition;

namespace DTS.Framework.Tests.Common
{
    internal class Music
    {
        public static Domain CreateMusicDomain()
        {
            Domain domain = new Domain("DTS.Music", new DomainOptions
            {
                AutoIdProperty = true,
                AutoIdPropertyType = typeof (Int16)
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

            return domain;
        }
    }
}


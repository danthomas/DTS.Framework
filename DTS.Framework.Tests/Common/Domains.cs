using System;
using DTS.Framework.DomainDefinition;

namespace DTS.Framework.Tests.Common
{
    internal class Domains
    {
        public static Domain CreateManagerDomain()
        {
            Domain domain = new Domain("DTS.Manager", new DomainOptions
            {
                AutoIdProperty = true,
                AutoIdPropertyType = typeof(Int16)
            })
            .AddDefaultDataTypes()
            .AddDataType(new CodeDataType());

            Group customers = domain.AddGroup("Customers");
            Group projects = domain.AddGroup("Projects");
            Group scheduling = domain.AddGroup("Scheduling");
            Group time = domain.AddGroup("Time");

            customers.AddEntity("Customer")
                .Value("Code", "Code", isUnique: true)
                .Value<string>("Name");

            projects.AddEntity("Project")
                .Value("Code", "Code", isUnique: true)
                .Value<string>("Name");


            return domain;
        }

        public static Domain CreateMusicDomain()
        {
            Domain domain = new Domain("DTS.Music", new DomainOptions
            {
                AutoIdProperty = true,
                AutoIdPropertyType = typeof(Int16)
            }).AddDefaultDataTypes();

            Group group = domain.AddGroup("Main");

            Entity genre = group.AddEntity("Genre")
                .Value<string>("Name", isUnique: true);

            Entity artist = group.AddEntity("Artist")
                .Value<string>("Name", isUnique: true);

            Entity album = group.AddEntity("Album")
                .Value<string>("Name")
                .Reference(artist)
                .Reference(genre, true)
                .Unique("ArtistId", "Name");

            Entity track = group.AddEntity<int>("Track")
                .Value<string>("Name")
                .Value<TimeSpan>("Length")
                .Reference(artist)
                .Reference(album)
                .Reference(genre, true)
                .Unique("AlbumId", "Name");

            Entity playlist = group.AddEntity("Playlist")
                .Value<string>("Name", isUnique: true);

            Entity playlistTrack = group.AddEntity<int>("PlaylistTrack")
                .Reference(playlist)
                .Reference(track)
                .Value<byte>("Order")
                .Unique("PlaylistId", "Order");

            return domain;
        }
    }
}


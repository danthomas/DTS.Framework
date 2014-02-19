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
                DefaultId = DefaultId.Auto,
                DefaultIdType = typeof(Int16)
            })
            .AddDefaultDataTypes()
            .AddDataType(new CodeDataType());

            Group security = domain.AddGroup("Security");
            Group companies = domain.AddGroup("Companies");
            Group projects = domain.AddGroup("Projects");
            Group scheduling = domain.AddGroup("Scheduling");
            Group time = domain.AddGroup("Time");

            Entity company = companies.AddEntity("Company")
                .Value("Code", "Code", isUnique: true)
                .Value<bool>("IsInternal")
                .Value<string>("Name", 30)
                .Value<bool>("IsActive", @default: true)
                .Value<bool>("IsDeleted")
                .Value<DateTime>("CreatedDateTime", @default: DefaultValue.Now)
                .Value<DateTime>("CreatedUser", @default: DefaultValue.CurrentUser)
                .Value<DateTime>("UpdatedDateTime", nullable:true)
                .Value<DateTime>("UpdatedUser", nullable: true);

            Entity user = security.AddEntity("User")
                .Value<string>("Username", 20, 6, isUnique: true)
                .Value<string>("Email", 200, 6, isUnique: true)
                .Value<string>("FirstName", 20)
                .Value<string>("MiddleName", 20)
                .Value<string>("LastName", 30)
                .Value<string>("PreferredName", 20)
                .Value<bool>("IsActive", @default: true)
                .Value<bool>("IsDeleted")
                .Value<DateTime>("CreatedDateTime", @default: DefaultValue.Now)
                .Value<DateTime>("CreatedUser", @default: DefaultValue.CurrentUser)
                .Value<DateTime>("UpdatedDateTime", nullable:true)
                .Value<DateTime>("UpdatedUser", nullable: true)
                .Reference(company);

            Entity role = security.AddEntity("Role")
                .Value<string>("Name", 20, 4, isUnique: true)
                .Value<bool>("IsExternal")
                .Value<bool>("IsDeleted")
                .Value<DateTime>("CreatedDateTime", @default: DefaultValue.Now)
                .Value<DateTime>("CreatedUser", @default: DefaultValue.CurrentUser)
                .Value<DateTime>("UpdatedDateTime", nullable: true)
                .Value<DateTime>("UpdatedUser", nullable: true);

            Entity userRole = security.AddEntity("UserRole")
                .Reference(user)
                .Reference(role);

            Entity project = projects.AddEntity("Project")
                .Value("Code", "Code", isUnique: true)
                .Value<string>("Name")
                .Value<bool>("IsActive", @default: true)
                .Value<bool>("IsDeleted")
                .Value<DateTime>("CreatedDateTime", @default: DefaultValue.Now)
                .Value<DateTime>("CreatedUser", @default: DefaultValue.CurrentUser)
                .Value<DateTime>("UpdatedDateTime", nullable:true)
                .Value<DateTime>("UpdatedUser", nullable: true)
                .Reference(company);

            Entity calendar = scheduling.AddEntity("Calendar")
                .Value<string>("Name")
                .Value<bool>("IsActive", @default: true)
                .Value<bool>("IsDeleted")
                .Value<DateTime>("CreatedDateTime", @default: DefaultValue.Now)
                .Value<DateTime>("CreatedUser", @default: DefaultValue.CurrentUser)
                .Value<DateTime>("UpdatedDateTime", nullable: true)
                .Value<DateTime>("UpdatedUser", nullable: true);

            Entity durationType = scheduling.AddEntity<byte>("DurationType", defaultId: DefaultId.Yes)
                .Value("Code", "Code", isUnique: true)
                .Value<string>("Name", 20)
                .Value<short>("ConversionFactor");

            Entity entryType = scheduling.AddEntity<byte>("EntryType", defaultId: DefaultId.Yes)
                .Value("Code", "Code", isUnique: true)
                .Value<string>("Name", 20)
                .Value<short>("ConversionFactor");

            Entity entry = scheduling.AddEntity<int>("Entry")
                .Reference(calendar)
                .Reference(user, nullable: true)
                .Value<DateTime>("DateTime")
                .Reference(entryType)
                .Reference(durationType)
                .Value<short>("Duration");


            return domain;
        }

        public static Domain CreateMusicDomain()
        {
            Domain domain = new Domain("DTS.Music", new DomainOptions
            {
                DefaultId = DefaultId.Auto,
                DefaultIdType = typeof(Int16)
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


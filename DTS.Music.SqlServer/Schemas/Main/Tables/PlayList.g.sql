create table [Main].[Playlist]
(
[PlaylistId] smallint NOT NULL IDENTITY (1, 1)
, [Name] varchar(50) NOT NULL
, CONSTRAINT [PK_Playlist] PRIMARY KEY ([PlaylistId])
, CONSTRAINT [AK_Playlist_Name] UNIQUE ([Name])
)
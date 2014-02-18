create table [Main].[PlaylistTrack]
(
[PlaylistTrackId] int NOT NULL IDENTITY (1, 1)
, [PlaylistId] smallint NOT NULL
, [TrackId] int NOT NULL
, [Order] tinyint NOT NULL
, CONSTRAINT [PK_PlaylistTrack] PRIMARY KEY ([PlaylistTrackId])
, CONSTRAINT [FK_PlaylistTrack_PlaylistId_Playlist] FOREIGN KEY ([PlaylistId]) REFERENCES [Main].[Playlist]([PlaylistId])
, CONSTRAINT [FK_PlaylistTrack_TrackId_Track] FOREIGN KEY ([TrackId]) REFERENCES [Main].[Track]([TrackId])
, CONSTRAINT [AK_PlaylistTrack_PlaylistId_Order] UNIQUE ([PlaylistId], [Order])
)
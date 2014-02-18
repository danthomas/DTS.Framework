create table [Main].[Track]
(
[TrackId] int NOT NULL IDENTITY (1, 1)
, [Name] varchar(50) NOT NULL
, [Length] time NOT NULL
, [ArtistId] smallint NOT NULL
, [AlbumId] smallint NOT NULL
, [GenreId] smallint NULL
, CONSTRAINT [PK_Track] PRIMARY KEY ([TrackId])
, CONSTRAINT [FK_Track_ArtistId_Artist] FOREIGN KEY ([ArtistId]) REFERENCES [Main].[Artist]([ArtistId])
, CONSTRAINT [FK_Track_AlbumId_Album] FOREIGN KEY ([AlbumId]) REFERENCES [Main].[Album]([AlbumId])
, CONSTRAINT [FK_Track_GenreId_Genre] FOREIGN KEY ([GenreId]) REFERENCES [Main].[Genre]([GenreId])
, CONSTRAINT [AK_Track_Name_AlbumId] UNIQUE ([Name], [AlbumId])
)
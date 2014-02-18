create table [Main].[Album]
(
[AlbumId] smallint NOT NULL IDENTITY (1, 1)
, [Name] varchar(50) NOT NULL
, [ArtistId] smallint NOT NULL
, [GenreId] smallint NULL
, CONSTRAINT [PK_Album] PRIMARY KEY ([AlbumId])
, CONSTRAINT [FK_Album_ArtistId_Artist] FOREIGN KEY ([ArtistId]) REFERENCES [Main].[Artist]([ArtistId])
, CONSTRAINT [FK_Album_GenreId_Genre] FOREIGN KEY ([GenreId]) REFERENCES [Main].[Genre]([GenreId])
, CONSTRAINT [AK_Album_Name_ArtistId] UNIQUE ([Name], [ArtistId])
)
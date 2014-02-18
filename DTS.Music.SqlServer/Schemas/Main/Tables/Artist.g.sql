create table [Main].[Artist]
(
[ArtistId] smallint NOT NULL IDENTITY (1, 1)
, [Name] varchar(50) NOT NULL
, CONSTRAINT [PK_Artist] PRIMARY KEY ([ArtistId])
, CONSTRAINT [AK_Artist_Name] UNIQUE ([Name])
)
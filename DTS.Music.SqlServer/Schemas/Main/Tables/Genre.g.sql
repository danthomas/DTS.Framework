create table [Main].[Genre]
(
[GenreId] smallint NOT NULL IDENTITY (1, 1)
, [Name] varchar(50) NOT NULL
, CONSTRAINT [PK_Genre] PRIMARY KEY ([GenreId])
, CONSTRAINT [AK_Genre_Name] UNIQUE ([Name])
)
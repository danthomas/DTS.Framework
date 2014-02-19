create table [Scheduling].[EntryType]
(
[EntryTypeId] tinyint NOT NULL
, [Code] varchar(5) NOT NULL
, [Name] varchar(20) NOT NULL
, [ConversionFactor] smallint NOT NULL
, CONSTRAINT [PK_EntryType] PRIMARY KEY ([EntryTypeId])
, CONSTRAINT [AK_EntryType_Code] UNIQUE ([Code])
, CONSTRAINT [Code_MinLength] CHECK (LEN([Code]) > 3)
)
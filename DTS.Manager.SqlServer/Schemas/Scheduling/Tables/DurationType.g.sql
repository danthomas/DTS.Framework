create table [Scheduling].[DurationType]
(
[DurationTypeId] tinyint NOT NULL
, [Code] varchar(5) NOT NULL
, [Name] varchar(20) NOT NULL
, [ConversionFactor] smallint NOT NULL
, CONSTRAINT [PRIMARYKEY_DurationType] PRIMARY KEY ([DurationTypeId])
, CONSTRAINT [UNIQUE_DurationType_Code] UNIQUE ([Code])
, CONSTRAINT [CHECK_DurationType_Code_MinLength] CHECK (LEN([Code]) > 3)
)
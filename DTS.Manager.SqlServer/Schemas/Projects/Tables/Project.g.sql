create table [Projects].[Project]
(
[ProjectId] smallint NOT NULL IDENTITY (1, 1)
, [Code] varchar(5) NOT NULL
, [Name] varchar(50) NOT NULL
, CONSTRAINT [PK_Project] PRIMARY KEY ([ProjectId])
, CONSTRAINT [AK_Project_Code] UNIQUE ([Code])
, CONSTRAINT [Code_MinLength] CHECK (LEN([Code]) > 3)
)
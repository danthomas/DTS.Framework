create table [Projects].[Project]
(
[ProjectId] smallint NOT NULL IDENTITY (1, 1)
, [Code] varchar(5) NOT NULL
, [Name] varchar(50) NOT NULL
, [IsActive] bit NOT NULL DEFAULT 1
, [IsDeleted] bit NOT NULL
, [CreatedDateTime] date NOT NULL DEFAULT getutcdate()
, [CreatedUser] date NOT NULL DEFAULT suser_sname()
, [UpdatedDateTime] date NULL
, [UpdatedUser] date NULL
, [CompanyId] smallint NOT NULL
, CONSTRAINT [PK_Project] PRIMARY KEY ([ProjectId])
, CONSTRAINT [FK_Project_CompanyId_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Companies].[Company]([CompanyId])
, CONSTRAINT [AK_Project_Code] UNIQUE ([Code])
, CONSTRAINT [Code_MinLength] CHECK (LEN([Code]) > 3)
)
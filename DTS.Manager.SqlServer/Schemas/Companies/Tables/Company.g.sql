create table [Companies].[Company]
(
[CompanyId] smallint NOT NULL IDENTITY (1, 1)
, [Code] varchar(5) NOT NULL
, [IsInternal] bit NOT NULL
, [Name] varchar(30) NOT NULL
, [IsActive] bit NOT NULL DEFAULT 1
, [IsDeleted] bit NOT NULL
, [CreatedDateTime] date NOT NULL DEFAULT getutcdate()
, [CreatedUser] date NOT NULL DEFAULT suser_sname()
, [UpdatedDateTime] date NULL
, [UpdatedUser] date NULL
, CONSTRAINT [PK_Company] PRIMARY KEY ([CompanyId])
, CONSTRAINT [AK_Company_Code] UNIQUE ([Code])
, CONSTRAINT [Code_MinLength] CHECK (LEN([Code]) > 3)
)
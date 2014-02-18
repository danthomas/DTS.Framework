create table [Security].[Role]
(
[RoleId] smallint NOT NULL IDENTITY (1, 1)
, [Name] varchar(20) NOT NULL
, [IsExternal] bit NOT NULL
, [IsDeleted] bit NOT NULL
, [CreatedDateTime] date NOT NULL DEFAULT getutcdate()
, [CreatedUser] date NOT NULL DEFAULT suser_sname()
, [UpdatedDateTime] date NULL
, [UpdatedUser] date NULL
, CONSTRAINT [PK_Role] PRIMARY KEY ([RoleId])
, CONSTRAINT [AK_Role_Name] UNIQUE ([Name])
, CONSTRAINT [Name_MinLength] CHECK (LEN([Name]) > 4)
)
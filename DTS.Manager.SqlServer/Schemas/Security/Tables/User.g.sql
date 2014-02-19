create table [Security].[User]
(
[UserId] smallint NOT NULL IDENTITY (1, 1)
, [Username] varchar(20) NOT NULL
, [Email] varchar(200) NOT NULL
, [FirstName] varchar(20) NOT NULL
, [MiddleName] varchar(20) NOT NULL
, [LastName] varchar(30) NOT NULL
, [PreferredName] varchar(20) NOT NULL
, [IsActive] bit NOT NULL DEFAULT 1
, [IsDeleted] bit NOT NULL
, [CreatedDateTime] date NOT NULL DEFAULT getutcdate()
, [CreatedUser] date NOT NULL DEFAULT suser_sname()
, [UpdatedDateTime] date NULL
, [UpdatedUser] date NULL
, [CompanyId] smallint NOT NULL
, CONSTRAINT [PK_User] PRIMARY KEY ([UserId])
, CONSTRAINT [FK_User_CompanyId_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Companies].[Company]([CompanyId])
, CONSTRAINT [AK_User_Username] UNIQUE ([Username])
, CONSTRAINT [AK_User_Email] UNIQUE ([Email])
, CONSTRAINT [Username_MinLength] CHECK (LEN([Username]) > 6)
, CONSTRAINT [Email_MinLength] CHECK (LEN([Email]) > 6)
)
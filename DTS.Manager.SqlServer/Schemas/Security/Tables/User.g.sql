create table [Security].[User]
(
[UserId] smallint not null identity (1, 1)
, [Username] varchar(20) not null
, [Email] varchar(200) not null
, [FirstName] varchar(20) not null
, [MiddleName] varchar(20) not null
, [LastName] varchar(30) not null
, [PreferredName] varchar(20) not null
, [IsActive] bit not null default 1
, [IsDeleted] bit not null
, [CompanyId] smallint not null
, [CreatedDateTime] date not null default getutcdate()
, [CreatedUser] date not null default suser_sname()
, [UpdatedDateTime] date null
, [UpdatedUser] date null
, constraint [PrimaryKey_User] primary key ([UserId])
, constraint [ForeignKey_User_CompanyId_Company] foreign key ([CompanyId]) references [Companies].[Company]([CompanyId])
, constraint [Unique_User_Username] unique ([Username])
, constraint [Unique_User_Email] unique ([Email])
, constraint [Check_User_Username_MinLength] check (len([Username]) > 6)
, constraint [Check_User_Email_MinLength] check (len([Email]) > 6)

)
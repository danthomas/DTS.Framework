create table [Security].[Role]
(
[RoleId] smallint not null identity (1, 1)
, [Name] varchar(20) not null
, [IsExternal] bit not null
, [IsDeleted] bit not null
, [CreatedDateTime] date not null default getutcdate()
, [CreatedUser] date not null default suser_sname()
, [UpdatedDateTime] date null
, [UpdatedUser] date null
, constraint [PrimaryKey_Role] primary key ([RoleId])
, constraint [Unique_Role_Name] unique ([Name])
, constraint [Check_Role_Name_MinLength] check (len([Name]) > 4)

)
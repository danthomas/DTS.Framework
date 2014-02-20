create table [Companies].[Company]
(
[CompanyId] smallint not null identity (1, 1)
, [Code] varchar(5) not null
, [IsInternal] bit not null
, [Name] varchar(30) not null
, [IsActive] bit not null default 1
, [IsDeleted] bit not null
, [CreatedDateTime] date not null default getutcdate()
, [CreatedUser] date not null default suser_sname()
, [UpdatedDateTime] date null
, [UpdatedUser] date null
, constraint [PrimaryKey_Company] primary key ([CompanyId])
, constraint [Unique_Company_Code] unique ([Code])
, constraint [Check_Company_Code_MinLength] check (len([Code]) > 3)

)
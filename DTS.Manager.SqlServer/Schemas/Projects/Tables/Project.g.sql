create table [Projects].[Project]
(
[ProjectId] smallint not null identity (1, 1)
, [Code] varchar(5) not null
, [Name] varchar(50) not null
, [IsActive] bit not null default 1
, [IsDeleted] bit not null
, [CompanyId] smallint not null
, [CreatedDateTime] date not null default getutcdate()
, [CreatedUser] date not null default suser_sname()
, [UpdatedDateTime] date null
, [UpdatedUser] date null
, constraint [PrimaryKey_Project] primary key ([ProjectId])
, constraint [ForeignKey_Project_CompanyId_Company] foreign key ([CompanyId]) references [Companies].[Company]([CompanyId])
, constraint [Unique_Project_Code] unique ([Code])
, constraint [Check_Project_Code_MinLength] check (len([Code]) > 3)

)
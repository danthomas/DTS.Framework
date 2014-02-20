create table [Scheduling].[EntryType]
(
[EntryTypeId] smallint not null identity (1, 1)
, [Code] varchar(5) not null
, [Name] varchar(20) not null
, constraint [PrimaryKey_EntryType] primary key ([EntryTypeId])
, constraint [Unique_EntryType_Code] unique ([Code])
, constraint [Check_EntryType_Code_MinLength] check (len([Code]) > 3)

)
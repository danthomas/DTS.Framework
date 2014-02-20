create table [Scheduling].[Calendar]
(
[CalendarId] smallint not null identity (1, 1)
, [Name] varchar(50) not null
, [IsActive] bit not null default 1
, [IsDeleted] bit not null
, [CreatedDateTime] date not null default getutcdate()
, [CreatedUser] date not null default suser_sname()
, [UpdatedDateTime] date null
, [UpdatedUser] date null
, constraint [PrimaryKey_Calendar] primary key ([CalendarId])

)
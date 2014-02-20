create table [Scheduling].[Entry]
(
[EntryId] int not null identity (1, 1)
, [CalendarId] smallint not null
, [UserId] smallint null
, [DateTime] date not null
, [EntryTypeId] smallint not null
, [Duration] smallint not null
, [CreatedDateTime] date not null default getutcdate()
, [CreatedUser] date not null default suser_sname()
, [UpdatedDateTime] date null
, [UpdatedUser] date null
, constraint [PrimaryKey_Entry] primary key ([EntryId])
, constraint [ForeignKey_Entry_CalendarId_Calendar] foreign key ([CalendarId]) references [Scheduling].[Calendar]([CalendarId])
, constraint [ForeignKey_Entry_UserId_User] foreign key ([UserId]) references [Security].[User]([UserId])
, constraint [ForeignKey_Entry_EntryTypeId_EntryType] foreign key ([EntryTypeId]) references [Scheduling].[EntryType]([EntryTypeId])

)
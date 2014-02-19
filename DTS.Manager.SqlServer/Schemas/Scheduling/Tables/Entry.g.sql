create table [Scheduling].[Entry]
(
[EntryId] int NOT NULL IDENTITY (1, 1)
, [CalendarId] smallint NOT NULL
, [UserId] smallint NULL
, [DateTime] date NOT NULL
, [EntryTypeId] tinyint NOT NULL
, [DurationTypeId] tinyint NOT NULL
, [Duration] smallint NOT NULL
, CONSTRAINT [PK_Entry] PRIMARY KEY ([EntryId])
, CONSTRAINT [FK_Entry_CalendarId_Calendar] FOREIGN KEY ([CalendarId]) REFERENCES [Scheduling].[Calendar]([CalendarId])
, CONSTRAINT [FK_Entry_UserId_User] FOREIGN KEY ([UserId]) REFERENCES [Security].[User]([UserId])
, CONSTRAINT [FK_Entry_EntryTypeId_EntryType] FOREIGN KEY ([EntryTypeId]) REFERENCES [Scheduling].[EntryType]([EntryTypeId])
, CONSTRAINT [FK_Entry_DurationTypeId_DurationType] FOREIGN KEY ([DurationTypeId]) REFERENCES [Scheduling].[DurationType]([DurationTypeId])
)
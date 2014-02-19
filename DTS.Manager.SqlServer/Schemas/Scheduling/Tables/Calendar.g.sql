create table [Scheduling].[Calendar]
(
[CalendarId] smallint NOT NULL IDENTITY (1, 1)
, [Name] varchar(50) NOT NULL
, [IsActive] bit NOT NULL DEFAULT 1
, [IsDeleted] bit NOT NULL
, [CreatedDateTime] date NOT NULL DEFAULT getutcdate()
, [CreatedUser] date NOT NULL DEFAULT suser_sname()
, [UpdatedDateTime] date NULL
, [UpdatedUser] date NULL
, CONSTRAINT [PK_Calendar] PRIMARY KEY ([CalendarId])
)
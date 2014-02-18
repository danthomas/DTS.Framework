create table [Security].[UserRole]
(
[UserRoleId] smallint NOT NULL IDENTITY (1, 1)
, [UserId] smallint NOT NULL
, [RoleId] smallint NOT NULL
, CONSTRAINT [PK_UserRole] PRIMARY KEY ([UserRoleId])
, CONSTRAINT [FK_UserRole_UserId_User] FOREIGN KEY ([UserId]) REFERENCES [Security].[User]([UserId])
, CONSTRAINT [FK_UserRole_RoleId_Role] FOREIGN KEY ([RoleId]) REFERENCES [Security].[Role]([RoleId])
)
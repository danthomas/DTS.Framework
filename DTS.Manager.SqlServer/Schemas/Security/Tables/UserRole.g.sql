create table [Security].[UserRole]
(
[UserRoleId] smallint not null identity (1, 1)
, [UserId] smallint not null
, [RoleId] smallint not null
, constraint [PrimaryKey_UserRole] primary key ([UserRoleId])
, constraint [ForeignKey_UserRole_UserId_User] foreign key ([UserId]) references [Security].[User]([UserId])
, constraint [ForeignKey_UserRole_RoleId_Role] foreign key ([RoleId]) references [Security].[Role]([RoleId])

)
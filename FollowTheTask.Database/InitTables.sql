SET IDENTITY_INSERT [dbo].[Roles] ON
INSERT INTO [dbo].[Roles]([Id], [Name])
    VALUES (1, 'Admin'), (2, 'Leader'), (3, 'Developer'), (4, 'User');
SET IDENTITY_INSERT [dbo].[Roles] OFF

SET IDENTITY_INSERT [dbo].[ActionSources] ON
INSERT INTO [dbo].[ActionSources]([Id], [Name])
    VALUES (1, 'UserAction'), (2, 'RequestApproved'), (3, 'RequestDeclined');
SET IDENTITY_INSERT [dbo].[ActionSources] OFF

INSERT INTO [dbo].[ActionTypes]([Name])
    VALUES ('JoinTeam'), ('SolveIssue'), ('BeLeader');

INSERT INTO [dbo].[IssueTypes]([Name])
    VALUES ('Story'), ('Improvement'), ('Bug'), ('Investigation');

INSERT INTO [dbo].[Priorities]([Name])
    VALUES ('Trivial'), ('Minor'), ('Normal'), ('Major'), ('Critical');

INSERT INTO [dbo].[Resolutions]([Name])
    VALUES ('Unknown'), ('CannotReproduce'), ('Done'), ('Fixed');

INSERT INTO [dbo].[Statuses]([Name])
    VALUES ('Open'), ('Reopened'), ('InProgress'), ('Resolved'), ('Closed');

SET IDENTITY_INSERT [dbo].[AuthData] ON
INSERT INTO [dbo].[AuthData] ([Id], [PasswordHash], [Salt], [SecurityStamp], [LockoutEnabled], [AccessFailedCnt], [LockoutDateUtc], [AccessGrantedTotal], [LastAccessGrantedDateUtc], [AccessFailedTotal], [LastAccessFailedDateUtc])
    VALUES (1, N'WFAIeXLpMA5P54dFiUrdkhS6WX3Mrf8SW4kdbHuBxMw=', N'an27BNjrDEme+GHDzIt+fw==', 'f8b4c821-78ee-4301-a33f-14f59b9c4f11', 0, 0, NULL, 0, NULL, 0, NULL);
SET IDENTITY_INSERT [dbo].[AuthData] OFF

SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([Id], [Username], [Email], [EmailConfirmed], [FirstName], [LastName], [AuthId])
    VALUES (1, NULL, 'admin@admin.com', 1, 'Lonely', 'Admin', 1);
SET IDENTITY_INSERT [dbo].[Users] OFF

INSERT INTO [dbo].[UserRoles]([UserId], [RoleId])
        VALUES (1, 1), (1, 4);

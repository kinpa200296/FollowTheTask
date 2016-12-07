CREATE TRIGGER RolesChanged ON [dbo].[Roles]
INSTEAD OF INSERT, UPDATE, DELETE AS
BEGIN
    RAISERROR ('Changing table Roles is not allowed', 10, 1); 
END;
GO

CREATE TRIGGER ActionSourcesChanged ON [dbo].[ActionSources]
INSTEAD OF INSERT, UPDATE, DELETE AS
BEGIN
    RAISERROR ('Changing table ActionSources is not allowed', 10, 1);
END;
GO

CREATE TRIGGER ActionTypesChanged ON [dbo].[ActionTypes]
INSTEAD OF INSERT, UPDATE, DELETE AS
BEGIN
    RAISERROR ('Changing table ActionTypes is not allowed', 10, 1);
END;
GO

CREATE TRIGGER IssueTypesChanged ON [dbo].[IssueTypes]
INSTEAD OF INSERT, UPDATE, DELETE AS
BEGIN
    RAISERROR ('Changing table IssueTypes is not allowed', 10, 1);
END;
GO

CREATE TRIGGER PrioritiesChanged ON [dbo].[Priorities]
INSTEAD OF INSERT, UPDATE, DELETE AS
BEGIN
    RAISERROR ('Changing table Priorities is not allowed', 10, 1);
END;
GO

CREATE TRIGGER ResolutionsChanged ON [dbo].[Resolutions]
INSTEAD OF INSERT, UPDATE, DELETE AS
BEGIN
    RAISERROR ('Changing table Resolutions is not allowed', 10, 1);
END;
GO

CREATE TRIGGER StatusesChanged ON [dbo].[Statuses]
INSTEAD OF INSERT, UPDATE, DELETE AS
BEGIN
    RAISERROR ('Changing table Statuses is not allowed', 10, 1);
END;
GO

CREATE TRIGGER UsersAfterInsert ON [dbo].[Users]
AFTER INSERT AS
BEGIN
    INSERT INTO [dbo].[UserRoles]([UserId], [RoleId])
        SELECT Id, [dbo].RoleIdUser() FROM inserted;
END;
GO

CREATE TRIGGER UsersInsteadOfDelete ON [dbo].[Users]
INSTEAD OF DELETE AS
BEGIN
    IF [dbo].UserIdAdmin() IN (SELECT Id FROM deleted)
        RAISERROR ('Deleting user "admin" is not allowed', 10, 1);

    UPDATE T SET UserId = NULL, UserName = [dbo].GetUserName(D.Id)
        FROM [dbo].[Comments] T, deleted D
            Where T.UserId = D.Id;

    UPDATE T SET ReporterId = NULL, ReporterName = [dbo].GetUserName(D.Id)
        FROM [dbo].[Issues] T, deleted D
            Where T.ReporterId = D.Id;

    UPDATE T SET AssigneeId = NULL, AssigneeName = [dbo].GetUserName(D.Id)
        FROM [dbo].[Issues] T, deleted D
            Where T.AssigneeId = D.Id;

    UPDATE T SET LeaderId = [dbo].UserIdAdmin()
        FROM [dbo].[Teams] T, deleted D
            Where T.LeaderId = D.Id;

    DELETE T
        FROM [dbo].[UserTeams] T, deleted D
            WHERE T.UserId = D.Id;

    DELETE T
        FROM [dbo].[Requests] T, deleted D
            WHERE T.SenderId = D.Id;

    UPDATE T SET ReceiverId = [dbo].UserIdAdmin()
        FROM [dbo].[Requests] T, deleted D
            Where T.ReceiverId = D.Id;

    DELETE T
        FROM [dbo].[Notifications] T, deleted D
            WHERE T.ReceiverId = D.Id;

    UPDATE T SET SenderId = [dbo].UserIdAdmin()
        FROM [dbo].[Notifications] T, deleted D
            Where T.SenderId = D.Id;

    DELETE T
        FROM [dbo].[UserRoles] T, deleted D
            WHERE T.UserId = D.Id;

    DELETE T
        FROM [dbo].[Users] T, deleted D
            WHERE T.Id = D.Id;

    DELETE T
        FROM [dbo].[AuthData] T, deleted D
            WHERE T.Id = D.AuthId;
END;
GO

CREATE TRIGGER TeamsInsteadOfInsert ON [dbo].[Teams]
INSTEAD OF INSERT AS
BEGIN
    INSERT INTO [dbo].[Teams]([Name], [LeaderId])
        SELECT Name, ISNULL(inserted.LeaderId, [dbo].UserIdAdmin()) FROM inserted;
END;
GO

CREATE TRIGGER TeamsInsteadOfDelete ON [dbo].[Teams]
INSTEAD OF DELETE AS
BEGIN
    DELETE T
        FROM [dbo].[Features] T, deleted D
            WHERE T.TeamId = D.Id;

    DELETE T
        FROM [dbo].[UserTeams] T, deleted D
            WHERE T.TeamId = D.Id;

    DELETE T
        FROM [dbo].[Teams] T, deleted D
            WHERE T.Id = D.Id;
END;
GO

CREATE TRIGGER FeaturesInsteadOfDelete ON [dbo].[Features]
INSTEAD OF DELETE AS
BEGIN
    DELETE T
        FROM [dbo].[Issues] T, deleted D
            WHERE T.FeatureId = D.Id;

    DELETE T
        FROM [dbo].[Features] T, deleted D
            WHERE T.Id = D.Id;
END;
GO

CREATE TRIGGER IssuesInsteadOfDelete ON [dbo].[Issues]
INSTEAD OF DELETE AS
BEGIN
    DELETE T
        FROM [dbo].[Comments] T, deleted D
            WHERE T.IssueId = D.Id;

    DELETE T
        FROM [dbo].[Issues] T, deleted D
            WHERE T.Id = D.Id;
END;
GO
CREATE TRIGGER RolesChanged ON [dbo].[Roles]
AFTER INSERT, UPDATE, DELETE AS
BEGIN
    RAISERROR ('Changing table Roles is not allowed', 10, 1);
    ROLLBACK TRANSACTION; 
END;
GO

CREATE TRIGGER ActionSourcesChanged ON [dbo].[ActionSources]
AFTER INSERT, UPDATE, DELETE AS
BEGIN
    RAISERROR ('Changing table ActionSources is not allowed', 10, 1);
    ROLLBACK TRANSACTION; 
END;
GO

CREATE TRIGGER ActionTypesChanged ON [dbo].[ActionTypes]
AFTER INSERT, UPDATE, DELETE AS
BEGIN
    RAISERROR ('Changing table ActionTypes is not allowed', 10, 1);
    ROLLBACK TRANSACTION; 
END;
GO

CREATE TRIGGER IssueTypesChanged ON [dbo].[IssueTypes]
AFTER INSERT, UPDATE, DELETE AS
BEGIN
    RAISERROR ('Changing table IssueTypes is not allowed', 10, 1);
    ROLLBACK TRANSACTION; 
END;
GO

CREATE TRIGGER PrioritiesChanged ON [dbo].[Priorities]
AFTER INSERT, UPDATE, DELETE AS
BEGIN
    RAISERROR ('Changing table Priorities is not allowed', 10, 1);
    ROLLBACK TRANSACTION; 
END;
GO

CREATE TRIGGER ResolutionsChanged ON [dbo].[Resolutions]
AFTER INSERT, UPDATE, DELETE AS
BEGIN
    RAISERROR ('Changing table Resolutions is not allowed', 10, 1);
    ROLLBACK TRANSACTION; 
END;
GO

CREATE TRIGGER StatusesChanged ON [dbo].[Statuses]
AFTER INSERT, UPDATE, DELETE AS
BEGIN
    RAISERROR ('Changing table Statuses is not allowed', 10, 1);
    ROLLBACK TRANSACTION; 
END;
GO

CREATE TRIGGER UsersAfterInsert ON [dbo].[Users]
AFTER INSERT AS
BEGIN
    DECLARE @UserId INT;
    SELECT @UserId = (SELECT [Id] FROM inserted);
    INSERT INTO [dbo].[UserRoles]([UserId], [RoleId])
        VALUES (@UserId, [dbo].[RoleIdUser]());
END;
GO

CREATE TRIGGER UsersInsteadOfDelete ON [dbo].[Users]
INSTEAD OF DELETE AS
BEGIN

    DELETE [dbo].[Users] FROM [dbo].[Users] AS T
        INNER JOIN deleted AS D ON T.Id = D.Id;

    DECLARE @AuthId int;
    SELECT @AuthId = (SELECT [AuthId] FROM deleted);
    DELETE FROM [dbo].[AuthData] WHERE [Id] = @AuthId;
END;
GO
CREATE FUNCTION UserIdAdmin()
RETURNS int AS
BEGIN
    RETURN 1;
END;
GO

CREATE FUNCTION RoleIdAdmin()
RETURNS int AS
BEGIN
    RETURN 1;
END;
GO

CREATE FUNCTION RoleIdLeader()
RETURNS int AS
BEGIN
    RETURN 2;
END;
GO

CREATE FUNCTION RoleIdDeveloper()
RETURNS int AS
BEGIN
    RETURN 3;
END;
GO

CREATE FUNCTION RoleIdUser()
RETURNS int AS
BEGIN
    RETURN 4;
END;
GO

CREATE FUNCTION ActionSourceIdUserAction()
RETURNS int AS
BEGIN
    RETURN 1;
END;
GO

CREATE FUNCTION ActionSourceIdRequestApproved()
RETURNS int AS
BEGIN
    RETURN 2;
END;
GO

CREATE FUNCTION ActionSourceIdRequestDeclined()
RETURNS int AS
BEGIN
    RETURN 3;
END;
GO

CREATE FUNCTION GetUserName(@UserId int)
RETURNS nvarchar(120) AS
BEGIN
    DECLARE @Result nvarchar(120);
    DECLARE @FirstName nvarchar(50);
    DECLARE @LastName nvarchar(70);
    SELECT @FirstName = (SELECT FirstName FROM [dbo].[Users] WHERE Id = @UserId);
    SELECT @LastName = (SELECT LastName FROM [dbo].[Users] WHERE Id = @UserId);
    IF @FirstName IS NOT NULL AND @LastName IS NOT NULL
        SET @Result = @FirstName + ' ' + @LastName;
    IF @FirstName IS NOT NULL AND @LastName IS NULL
        SET @Result = @FirstName;
    IF @FirstName IS NULL AND @LastName IS NOT NULL
        SET @Result = @LastName;
    IF @FirstName IS NULL AND @LastName IS NULL
        SET @Result = (SELECT Username FROM [dbo].[Users] WHERE Id = @UserId);
    RETURN @Result;
END;
GO

CREATE PROCEDURE SendRequest(@TargetId int, @ActionTypeId int, @SenderId int, @ReceiverId int)
AS INSERT INTO [dbo].[Requests](TargetId, ActionTypeId, SenderId, ReceiverId)
    VALUES (@TargetId, @ActionTypeId, @SenderId, @ReceiverId);
GO

CREATE PROCEDURE SendNotification(@TargetId int, @ActionSourceId int, @ActionTypeId int, @SenderId int, @ReceiverId int)
AS INSERT INTO [dbo].[Notifications](TargetId, ActionSourceId, ActionTypeId, SenderId, ReceiverId)
    VALUES (@TargetId, @ActionSourceId, @ActionTypeId, @SenderId, @ReceiverId);
GO
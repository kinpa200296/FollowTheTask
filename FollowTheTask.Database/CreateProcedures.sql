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

CREATE FUNCTION ActionTypeIdBeLeader()
RETURNS int AS
BEGIN
    RETURN 1;
END;
GO

CREATE FUNCTION ActionTypeIdJoinTeam()
RETURNS int AS
BEGIN
    RETURN 2;
END;
GO

CREATE FUNCTION ActionTypeIdAssignIssue()
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

CREATE FUNCTION GetTeamName(@TeamId int)
RETURNS nvarchar(100) AS
BEGIN
    DECLARE @Result nvarchar(100);
    SELECT @Result = (SELECT Name FROM [dbo].[Teams] WHERE Id = @TeamId);
    RETURN @Result;
END;
GO

CREATE FUNCTION GetFeatureName(@FeatureId int)
RETURNS nvarchar(100) AS
BEGIN
    DECLARE @Result nvarchar(100);
    SELECT @Result = (SELECT Name FROM [dbo].[Features] WHERE Id = @FeatureId);
    RETURN @Result;
END;
GO

CREATE FUNCTION GetIssueName(@IssueId int)
RETURNS nvarchar(100) AS
BEGIN
    DECLARE @Result nvarchar(100);
    SELECT @Result = (SELECT Name FROM [dbo].[Issues] WHERE Id = @IssueId);
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

CREATE FUNCTION ResolveTarget(@TargetId int, @ActionTypeId int)
RETURNS nvarchar(200) AS
BEGIN
    DECLARE @Result nvarchar(200);
    SELECT @Result = (SELECT CASE @ActionTypeId
    WHEN [dbo].ActionTypeIdBeLeader() THEN [dbo].GetTeamName(@TargetId)
    WHEN [dbo].ActionTypeIdJoinTeam() THEN [dbo].GetTeamName(@TargetId)
    WHEN [dbo].ActionTypeIdAssignIssue() THEN [dbo].GetIssueName(@TargetId)
    ELSE '<Unknown Target>'
    END);
    RETURN @Result;
END;
GO

CREATE FUNCTION GetTeam(@TeamId int)
RETURNS @Result table(
    [Id] int NOT NULL,
    [Name] nvarchar(100) NULL,
    [LeaderId] int NOT NULL,
    [Leader] nvarchar(120) NULL
) AS
BEGIN
    INSERT INTO @Result
        SELECT Id, Name, LeaderId, [dbo].GetUserName(T.LeaderId)
            FROM [dbo].[Teams] T WHERE Id = @TeamId;
    RETURN;
END;
GO

CREATE FUNCTION GetUserTeams(@UserId int)
RETURNS @Result table(
    [Id] int NOT NULL,
    [Name] nvarchar(100) NULL,
    [LeaderId] int NOT NULL,
    [Leader] nvarchar(120) NULL
) AS
BEGIN
    INSERT INTO @Result
        SELECT Id, Name, LeaderId, [dbo].GetUserName(T.LeaderId)
            FROM [dbo].[Teams] T WHERE Id IN 
                (SELECT TeamId FROM [dbo].[UserTeams] WHERE UserId = @UserId);
    RETURN;
END;
GO

CREATE FUNCTION GetLeaderTeams(@LeaderId int)
RETURNS @Result table(
    [Id] int NOT NULL,
    [Name] nvarchar(100) NULL,
    [LeaderId] int NOT NULL,
    [Leader] nvarchar(120) NULL
) AS
BEGIN
    INSERT INTO @Result
        SELECT Id, Name, LeaderId, [dbo].GetUserName(T.LeaderId)
            FROM [dbo].[Teams] T WHERE LeaderId = @LeaderId;
    RETURN;
END;
GO

CREATE FUNCTION GetAllTeams()
RETURNS @Result table(
    [Id] int NOT NULL,
    [Name] nvarchar(100) NULL,
    [LeaderId] int NOT NULL,
    [Leader] nvarchar(120) NULL
) AS
BEGIN
    INSERT INTO @Result
        SELECT Id, Name, LeaderId, [dbo].GetUserName(T.LeaderId)
            FROM [dbo].[Teams] T;
    RETURN;
END;
GO

CREATE FUNCTION GetTeamMembers(@TeamId int)
RETURNS @Result table(
    [Id] int NOT NULL,
    [Name] nvarchar(120) NULL
) AS
BEGIN
    INSERT INTO @Result
        SELECT UserId, [dbo].GetUserName(T.UserId)
            FROM [dbo].[UserTeams] T WHERE TeamId = @TeamId;
    RETURN;
END;
GO

CREATE FUNCTION GetFeature(@FeatureId int)
RETURNS @Result table(
    [Id] int NOT NULL,
    [Name] nvarchar(100) NULL,
    [Description] nvarchar(2000) NULL,
    [Version] nvarchar(50) NULL,
    [TeamId] int NOT NULL,
    [Team] nvarchar(100) NULL
) AS
BEGIN
    INSERT INTO @Result
        SELECT Id, Name, [Description], [Version], TeamId, [dbo].GetTeamName(T.TeamId)
            FROM [dbo].[Features] T WHERE Id = @FeatureId;
    RETURN;
END;
GO

CREATE FUNCTION GetFeatures(@TeamId int)
RETURNS @Result table(
    [Id] int NOT NULL,
    [Name] nvarchar(100) NULL,
    [Description] nvarchar(2000) NULL,
    [Version] nvarchar(50) NULL,
    [TeamId] int NOT NULL,
    [Team] nvarchar(100) NULL
) AS
BEGIN
    INSERT INTO @Result
        SELECT Id, Name, [Description], [Version], TeamId, [dbo].GetTeamName(T.TeamId)
            FROM [dbo].[Features] T WHERE TeamId = @TeamId;
    RETURN;
END;
GO

CREATE FUNCTION GetIssue(@IssueId int)
RETURNS @Result table(
    [Id] int NOT NULL,
    [Name] nvarchar(100) NULL,
    [Description] nvarchar(3000) NULL,
    [Version] nvarchar(50) NULL,
    [TypeId] int NULL,
    [PriorityId] int NULL,
    [StatusId] int NULL,
    [ResolutionId] int NULL,
    [CreatedDateUtc] datetimeoffset NOT NULL,
    [DeadlineDateUtc] datetimeoffset NULL,
    [FeatureId] int NOT NULL,
    [Feature] nvarchar(100) NULL,
    [ReporterName] nvarchar(120) NULL,
    [ReporterId] int NULL,
    [Reporter] nvarchar(120) NULL,
    [AssigneeName] nvarchar(120) NULL,
    [AssigneeId] int NULL,
    [Assignee] nvarchar(120) NULL
) AS
BEGIN
    INSERT INTO @Result
        SELECT Id, Name, [Description], [Version], TypeId, PriorityId, StatusId, ResolutionId,
            CreatedDateUtc, DeadlineDateUtc, FeatureId, [dbo].GetFeatureName(T.FeatureId),
            ReporterName, ReporterId, [dbo].GetUserName(T.ReporterId),
            AssigneeName, AssigneeId, [dbo].GetUserName(T.AssigneeId)
            FROM [dbo].[Issues] T WHERE Id = @IssueId;
    RETURN;
END;
GO

CREATE FUNCTION GetIssues(@FeatureId int)
RETURNS @Result table(
    [Id] int NOT NULL,
    [Name] nvarchar(100) NULL,
    [Description] nvarchar(3000) NULL,
    [Version] nvarchar(50) NULL,
    [TypeId] int NULL,
    [PriorityId] int NULL,
    [StatusId] int NULL,
    [ResolutionId] int NULL,
    [CreatedDateUtc] datetimeoffset NOT NULL,
    [DeadlineDateUtc] datetimeoffset NULL,
    [FeatureId] int NOT NULL,
    [Feature] nvarchar(100) NULL,
    [ReporterName] nvarchar(120) NULL,
    [ReporterId] int NULL,
    [Reporter] nvarchar(120) NULL,
    [AssigneeName] nvarchar(120) NULL,
    [AssigneeId] int NULL,
    [Assignee] nvarchar(120) NULL
) AS
BEGIN
    INSERT INTO @Result
        SELECT Id, Name, [Description], [Version], TypeId, PriorityId, StatusId, ResolutionId,
            CreatedDateUtc, DeadlineDateUtc, FeatureId, [dbo].GetFeatureName(T.FeatureId),
            ReporterName, ReporterId, [dbo].GetUserName(T.ReporterId),
            AssigneeName, AssigneeId, [dbo].GetUserName(T.AssigneeId)
            FROM [dbo].[Issues] T WHERE FeatureId = @FeatureId;
    RETURN;
END;
GO

CREATE FUNCTION GetComment(@CommentId int)
RETURNS @Result table(
    [Id] int NOT NULL,
    [Message] nvarchar(700) NULL,
    [DateCreatedUtc] datetimeoffset NOT NULL,
    [UserName] nvarchar(120) NULL,
    [UserId] int NULL,
    [User] nvarchar(120) NULL,
    [IssueId] int NOT NULL,
    [Issue] nvarchar(100) NULL
) AS
BEGIN
    INSERT INTO @Result
        SELECT Id, [Message], DateCreatedUtc, UserName, UserId, [dbo].GetUserName(T.UserId),
            IssueId, [dbo].GetIssueName(T.IssueId)
            FROM [dbo].[Comments] T WHERE Id = @CommentId;
    RETURN;
END;
GO

CREATE FUNCTION GetComments(@IssueId int)
RETURNS @Result table(
    [Id] int NOT NULL,
    [Message] nvarchar(700) NULL,
    [DateCreatedUtc] datetimeoffset NOT NULL,
    [UserName] nvarchar(120) NULL,
    [UserId] int NULL,
    [User] nvarchar(120) NULL,
    [IssueId] int NOT NULL,
    [Issue] nvarchar(100) NULL
) AS
BEGIN
    INSERT INTO @Result
        SELECT Id, [Message], DateCreatedUtc, UserName, UserId, [dbo].GetUserName(T.UserId),
            IssueId, [dbo].GetIssueName(T.IssueId)
            FROM [dbo].[Comments] T WHERE IssueId = @IssueId;
    RETURN;
END;
GO

CREATE FUNCTION GetRequest(@RequestId int)
RETURNS @Result table(
    [Id] int NOT NULL,
    [TargetId] int NOT NULL,
    [Target] nvarchar(200),
    [ActionTypeId] int NOT NULL,
    [SenderId] int NOT NULL,
    [Sender] nvarchar(120),
    [ReceiverId] int NOT NULL,
    [Receiver] nvarchar(120)
) AS
BEGIN
    INSERT INTO @Result
        SELECT Id, TargetId, [dbo].ResolveTarget(T.TargetId, T.ActionTypeId), ActionTypeId,
            SenderId, [dbo].GetUserName(T.SenderId),
            ReceiverId, [dbo].GetUserName(T.ReceiverId)
            FROM [dbo].[Requests] T WHERE Id = @RequestId;
    RETURN;
END;
GO

CREATE FUNCTION GetRequests(@UserId int)
RETURNS @Result table(
    [Id] int NOT NULL,
    [TargetId] int NOT NULL,
    [Target] nvarchar(200),
    [ActionTypeId] int NOT NULL,
    [SenderId] int NOT NULL,
    [Sender] nvarchar(120),
    [ReceiverId] int NOT NULL,
    [Receiver] nvarchar(120)
) AS
BEGIN
    INSERT INTO @Result
        SELECT Id, TargetId, [dbo].ResolveTarget(T.TargetId, T.ActionTypeId), ActionTypeId,
            SenderId, [dbo].GetUserName(T.SenderId),
            ReceiverId, [dbo].GetUserName(T.ReceiverId)
            FROM [dbo].[Requests] T WHERE SenderId = @UserId;
    RETURN;
END;
GO

CREATE FUNCTION GetPendingRequests(@UserId int)
RETURNS @Result table(
    [Id] int NOT NULL,
    [TargetId] int NOT NULL,
    [Target] nvarchar(200),
    [ActionTypeId] int NOT NULL,
    [SenderId] int NOT NULL,
    [Sender] nvarchar(120),
    [ReceiverId] int NOT NULL,
    [Receiver] nvarchar(120)
) AS
BEGIN
    INSERT INTO @Result
        SELECT Id, TargetId, [dbo].ResolveTarget(T.TargetId, T.ActionTypeId), ActionTypeId,
            SenderId, [dbo].GetUserName(T.SenderId),
            ReceiverId, [dbo].GetUserName(T.ReceiverId)
            FROM [dbo].[Requests] T WHERE ReceiverId = @UserId;
    RETURN;
END;
GO

CREATE FUNCTION GetNotification(@NotificationId int)
RETURNS @Result table(
    [Id] int NOT NULL,
    [TargetId] int NOT NULL,
    [Target] nvarchar(200),
    [ActionSourceId] int NOT NULL,
    [ActionTypeId] int NOT NULL,
    [SenderId] int NOT NULL,
    [Sender] nvarchar(120),
    [ReceiverId] int NOT NULL,
    [Receiver] nvarchar(120)
) AS
BEGIN
    INSERT INTO @Result
        SELECT Id, TargetId, [dbo].ResolveTarget(T.TargetId, T.ActionTypeId), ActionSourceId, ActionTypeId,
            SenderId, [dbo].GetUserName(T.SenderId),
            ReceiverId, [dbo].GetUserName(T.ReceiverId)
            FROM [dbo].[Notifications] T WHERE Id = @NotificationId;
    RETURN;
END;
GO

CREATE FUNCTION GetNotifications(@UserId int)
RETURNS @Result table(
    [Id] int NOT NULL,
    [TargetId] int NOT NULL,
    [Target] nvarchar(200),
    [ActionSourceId] int NOT NULL,
    [ActionTypeId] int NOT NULL,
    [SenderId] int NOT NULL,
    [Sender] nvarchar(120),
    [ReceiverId] int NOT NULL,
    [Receiver] nvarchar(120)
) AS
BEGIN
    INSERT INTO @Result
        SELECT Id, TargetId, [dbo].ResolveTarget(T.TargetId, T.ActionTypeId), ActionSourceId, ActionTypeId,
            SenderId, [dbo].GetUserName(T.SenderId),
            ReceiverId, [dbo].GetUserName(T.ReceiverId)
            FROM [dbo].[Notifications] T WHERE ReceiverId = @UserId;
    RETURN;
END;
GO
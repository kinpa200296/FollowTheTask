CREATE TABLE [dbo].[AuthData] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PasswordHash] nvarchar(70) NULL,
    [Salt] nvarchar(30) NULL,
    [SecurityStamp] nvarchar(50) NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCnt] int NOT NULL,
    [LockoutDateUtc] datetimeoffset NULL,
    [AccessGrantedTotal] int NOT NULL,
    [LastAccessGrantedDateUtc] datetimeoffset NULL,
    [AccessFailedTotal] int NOT NULL,
    [LastAccessFailedDateUtc] datetimeoffset NULL

    CONSTRAINT [PK_AuthData] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(50) NULL,
    [Email] nvarchar(70) NULL,
    [EmailConfirmed] bit NOT NULL,
    [FirstName] nvarchar(50) NULL,
    [LastName] nvarchar(70) NULL,
    [AuthId] int NOT NULL

    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
    CONSTRAINT [FK_Users_AuthId] FOREIGN KEY ([AuthId]) REFERENCES [dbo].[AuthData]([Id])
);
GO

CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50) NULL

    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[UserRoles] (
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL

    CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([UserId], [RoleId] ASC)
    CONSTRAINT [FK_UserRoles_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]),
    CONSTRAINT [FK_UserRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles]([Id])
);
GO

CREATE TABLE [dbo].[ActionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100) NULL

    CONSTRAINT [PK_ActionTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Requests] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TargetId] int NOT NULL,
    [ActionTypeId] int NOT NULL,
    [SenderId] int NOT NULL,
    [ReceiverId] int NOT NULL

    CONSTRAINT [PK_Requests] PRIMARY KEY CLUSTERED ([Id] ASC)
    CONSTRAINT [FK_Requests_ActionTypeId] FOREIGN KEY ([ActionTypeId]) REFERENCES [dbo].[ActionTypes]([Id]),
    CONSTRAINT [FK_Requests_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Users]([Id]),
    CONSTRAINT [FK_Requests_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [dbo].[Users]([Id])
);
GO

CREATE TABLE [dbo].[Notifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TargetId] int NOT NULL,
    [ActionTypeId] int NOT NULL,
    [SenderId] int NOT NULL,
    [ReceiverId] int NOT NULL

    CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED ([Id] ASC)
    CONSTRAINT [FK_Notifications_ActionTypeId] FOREIGN KEY ([ActionTypeId]) REFERENCES [dbo].[ActionTypes]([Id]),
    CONSTRAINT [FK_Notifications_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Users]([Id]),
    CONSTRAINT [FK_Notifications_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [dbo].[Users]([Id])
);
GO

CREATE TABLE [dbo].[Leaders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int NOT NULL

    CONSTRAINT [PK_Leaders] PRIMARY KEY CLUSTERED ([Id] ASC)
    CONSTRAINT [FK_Leaders_UserId] FOREIGN KEY ([Id]) REFERENCES [dbo].[Users]([Id])
);
GO

CREATE TABLE [dbo].[Teams] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100) NULL,
    [LeaderId] int NOT NULL

    CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED ([Id] ASC)
    CONSTRAINT [FK_Teams_LeaderId] FOREIGN KEY ([LeaderId]) REFERENCES [dbo].[Leaders]([Id])
);
GO

CREATE TABLE [dbo].[UserTeams] (
    [TeamId] int NOT NULL,
    [UserId] int NOT NULL

    CONSTRAINT [PK_UserTeams] PRIMARY KEY CLUSTERED ([TeamId], [UserId] ASC)
    CONSTRAINT [FK_UserTeams_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams]([Id]),
    CONSTRAINT [FK_UserTeams_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id])
);
GO

CREATE TABLE [dbo].[Features] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100) NULL,
    [Description] nvarchar(2000) NULL,
    [Version] nvarchar(50) NULL,
    [TeamId] int NOT NULL

    CONSTRAINT [PK_Features] PRIMARY KEY CLUSTERED ([Id] ASC)
    CONSTRAINT [FK_Features_TeamId] FOREIGN KEY ([Id]) REFERENCES [dbo].[Teams]([Id])
);
GO

CREATE TABLE [dbo].[IssueTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(70) NULL

    CONSTRAINT [PK_IssueTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Priorities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(70) NULL

    CONSTRAINT [PK_Priorities] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Resolutions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(70) NULL

    CONSTRAINT [PK_Resolutions] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Statuses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(70) NULL

    CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Issues] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100) NULL,
    [Description] nvarchar(3000) NULL,
    [Version] nvarchar(50) NULL,
    [TypeId] int NULL,
    [PriorityId] int NULL,
    [StatusId] int NULL,
    [ResolutionId] int NULL,
    [CreatedDateUtc] datetimeoffset NOT NULL,
    [DeadlineDateUtc] datetimeoffset NOT NULL,
    [FeatureId] int NOT NULL,
    [ReporterName] nvarchar(120) NULL,
    [ReporterId] int NOT NULL,
    [AssigneeName] nvarchar(120) NULL,
    [AssigneeId] int NOT NULL

    CONSTRAINT [PK_Issues] PRIMARY KEY CLUSTERED ([Id] ASC)
    CONSTRAINT [FK_Issues_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[IssueTypes]([Id]),
    CONSTRAINT [FK_Issues_PriorityId] FOREIGN KEY ([PriorityId]) REFERENCES [dbo].[Priorities]([Id]),
    CONSTRAINT [FK_Issues_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Statuses]([Id]),
    CONSTRAINT [FK_Issues_ResolutionId] FOREIGN KEY ([ResolutionId]) REFERENCES [dbo].[Resolutions]([Id]),
    CONSTRAINT [FK_Issues_FeatureId] FOREIGN KEY ([FeatureId]) REFERENCES [dbo].[Features]([Id]),
    CONSTRAINT [FK_Issues_ReporterId] FOREIGN KEY ([ReporterId]) REFERENCES [dbo].[Users]([Id]),
    CONSTRAINT [FK_Issues_AssigneeId] FOREIGN KEY ([AssigneeId]) REFERENCES [dbo].[Users]([Id])
);
GO

CREATE TABLE [dbo].[Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Message] nvarchar(700) NULL,
    [DateCreatedUtc] datetimeoffset NOT NULL,
    [UserId] int NOT NULL,
    [IssueId] int NOT NULL

    CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED ([Id] ASC)
    CONSTRAINT [FK_Comments_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]),
    CONSTRAINT [FK_Comments_IssueId] FOREIGN KEY ([IssueId]) REFERENCES [dbo].[Issues]([Id])
);
GO

-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/04/2016 22:30:53
-- Generated from EDMX file: E:\workspace\github\FollowTheTask\FollowTheTask.DAL\FollowTheTaskModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [test];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_Comments_dbo_Issues_IssueId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_dbo_Comments_dbo_Issues_IssueId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Comments_dbo_Users_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_dbo_Comments_dbo_Users_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Features_dbo_Teams_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Features] DROP CONSTRAINT [FK_dbo_Features_dbo_Teams_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Features_dbo_Teams_TeamEntity_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Features] DROP CONSTRAINT [FK_dbo_Features_dbo_Teams_TeamEntity_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Issues_dbo_Features_FeatureId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Issues] DROP CONSTRAINT [FK_dbo_Issues_dbo_Features_FeatureId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Issues_dbo_IssueTypes_TypeId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Issues] DROP CONSTRAINT [FK_dbo_Issues_dbo_IssueTypes_TypeId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Issues_dbo_Priorities_PriorityId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Issues] DROP CONSTRAINT [FK_dbo_Issues_dbo_Priorities_PriorityId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Issues_dbo_Resolutions_ResolutionId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Issues] DROP CONSTRAINT [FK_dbo_Issues_dbo_Resolutions_ResolutionId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Issues_dbo_Statuses_StatusId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Issues] DROP CONSTRAINT [FK_dbo_Issues_dbo_Statuses_StatusId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Issues_dbo_Users_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Issues] DROP CONSTRAINT [FK_dbo_Issues_dbo_Users_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Leaders_dbo_Users_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Leaders] DROP CONSTRAINT [FK_dbo_Leaders_dbo_Users_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Notifications_dbo_ActionTypes_ActionTypeId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notifications] DROP CONSTRAINT [FK_dbo_Notifications_dbo_ActionTypes_ActionTypeId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Notifications_dbo_Users_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notifications] DROP CONSTRAINT [FK_dbo_Notifications_dbo_Users_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Requests_dbo_ActionTypes_ActionTypeId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Requests] DROP CONSTRAINT [FK_dbo_Requests_dbo_ActionTypes_ActionTypeId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Requests_dbo_Users_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Requests] DROP CONSTRAINT [FK_dbo_Requests_dbo_Users_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_RoleEntityUserEntities_dbo_Roles_RoleEntity_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleEntityUserEntities] DROP CONSTRAINT [FK_dbo_RoleEntityUserEntities_dbo_Roles_RoleEntity_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_RoleEntityUserEntities_dbo_Users_UserEntity_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleEntityUserEntities] DROP CONSTRAINT [FK_dbo_RoleEntityUserEntities_dbo_Users_UserEntity_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_TeamEntityUserEntities_dbo_Teams_TeamEntity_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TeamEntityUserEntities] DROP CONSTRAINT [FK_dbo_TeamEntityUserEntities_dbo_Teams_TeamEntity_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_TeamEntityUserEntities_dbo_Users_UserEntity_Id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TeamEntityUserEntities] DROP CONSTRAINT [FK_dbo_TeamEntityUserEntities_dbo_Users_UserEntity_Id];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Teams_dbo_Leaders_LeaderId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Teams] DROP CONSTRAINT [FK_dbo_Teams_dbo_Leaders_LeaderId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Users_dbo_AuthData_AuthId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_dbo_Users_dbo_AuthData_AuthId];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ActionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionTypes];
GO
IF OBJECT_ID(N'[dbo].[AuthData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuthData];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[Features]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Features];
GO
IF OBJECT_ID(N'[dbo].[Issues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Issues];
GO
IF OBJECT_ID(N'[dbo].[IssueTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IssueTypes];
GO
IF OBJECT_ID(N'[dbo].[Leaders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Leaders];
GO
IF OBJECT_ID(N'[dbo].[Notifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notifications];
GO
IF OBJECT_ID(N'[dbo].[Priorities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Priorities];
GO
IF OBJECT_ID(N'[dbo].[Requests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Requests];
GO
IF OBJECT_ID(N'[dbo].[Resolutions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Resolutions];
GO
IF OBJECT_ID(N'[dbo].[RoleEntityUserEntities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleEntityUserEntities];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Statuses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Statuses];
GO
IF OBJECT_ID(N'[dbo].[TeamEntityUserEntities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TeamEntityUserEntities];
GO
IF OBJECT_ID(N'[dbo].[Teams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Teams];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ActionTypes'
CREATE TABLE [dbo].[ActionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL
);
GO

-- Creating table 'AuthDatas'
CREATE TABLE [dbo].[AuthDatas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [Salt] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCnt] int  NOT NULL,
    [LockoutDateUtc] datetimeoffset  NULL,
    [AccessGrantedTotal] int  NOT NULL,
    [LastAccessGrantedDateUtc] datetimeoffset  NULL,
    [AccessFailedTotal] int  NOT NULL,
    [LastAccessFailedDateUtc] datetimeoffset  NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Message] nvarchar(max)  NULL,
    [DateCreatedUtc] nvarchar(max)  NULL,
    [UserId] int  NOT NULL,
    [IssueId] int  NOT NULL
);
GO

-- Creating table 'Features'
CREATE TABLE [dbo].[Features] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [Version] nvarchar(max)  NULL,
    [TeamId] int  NOT NULL,
    [TeamEntity_Id] int  NULL
);
GO

-- Creating table 'Issues'
CREATE TABLE [dbo].[Issues] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [Version] nvarchar(max)  NULL,
    [TypeId] int  NULL,
    [PriorityId] int  NULL,
    [StatusId] int  NULL,
    [ResolutionId] int  NULL,
    [CreatedDateUtc] datetimeoffset  NOT NULL,
    [DeadlineDateUtc] datetimeoffset  NOT NULL,
    [FeatureId] int  NOT NULL,
    [ReporterName] nvarchar(max)  NULL,
    [ReporterId] int  NOT NULL,
    [AssigneeName] nvarchar(max)  NULL,
    [AssigneeId] int  NOT NULL
);
GO

-- Creating table 'IssueTypes'
CREATE TABLE [dbo].[IssueTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL
);
GO

-- Creating table 'Leaders'
CREATE TABLE [dbo].[Leaders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Notifications'
CREATE TABLE [dbo].[Notifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TargetId] int  NOT NULL,
    [ActionTypeId] int  NOT NULL,
    [SenderId] int  NOT NULL,
    [ReceiverId] int  NOT NULL
);
GO

-- Creating table 'Priorities'
CREATE TABLE [dbo].[Priorities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL
);
GO

-- Creating table 'Requests'
CREATE TABLE [dbo].[Requests] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TargetId] int  NOT NULL,
    [ActionTypeId] int  NOT NULL,
    [SenderId] int  NOT NULL,
    [ReceiverId] int  NOT NULL
);
GO

-- Creating table 'Resolutions'
CREATE TABLE [dbo].[Resolutions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL
);
GO

-- Creating table 'Statuses'
CREATE TABLE [dbo].[Statuses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL
);
GO

-- Creating table 'Teams'
CREATE TABLE [dbo].[Teams] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [LeaderId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [AuthId] int  NOT NULL
);
GO

-- Creating table 'RoleEntityUserEntities'
CREATE TABLE [dbo].[RoleEntityUserEntities] (
    [Roles_Id] int  NOT NULL,
    [Users_Id] int  NOT NULL
);
GO

-- Creating table 'TeamEntityUserEntities'
CREATE TABLE [dbo].[TeamEntityUserEntities] (
    [Teams_Id] int  NOT NULL,
    [Users_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ActionTypes'
ALTER TABLE [dbo].[ActionTypes]
ADD CONSTRAINT [PK_ActionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AuthDatas'
ALTER TABLE [dbo].[AuthDatas]
ADD CONSTRAINT [PK_AuthDatas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Features'
ALTER TABLE [dbo].[Features]
ADD CONSTRAINT [PK_Features]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Issues'
ALTER TABLE [dbo].[Issues]
ADD CONSTRAINT [PK_Issues]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IssueTypes'
ALTER TABLE [dbo].[IssueTypes]
ADD CONSTRAINT [PK_IssueTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Leaders'
ALTER TABLE [dbo].[Leaders]
ADD CONSTRAINT [PK_Leaders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [PK_Notifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Priorities'
ALTER TABLE [dbo].[Priorities]
ADD CONSTRAINT [PK_Priorities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [PK_Requests]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Resolutions'
ALTER TABLE [dbo].[Resolutions]
ADD CONSTRAINT [PK_Resolutions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Statuses'
ALTER TABLE [dbo].[Statuses]
ADD CONSTRAINT [PK_Statuses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Teams'
ALTER TABLE [dbo].[Teams]
ADD CONSTRAINT [PK_Teams]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Roles_Id], [Users_Id] in table 'RoleEntityUserEntities'
ALTER TABLE [dbo].[RoleEntityUserEntities]
ADD CONSTRAINT [PK_RoleEntityUserEntities]
    PRIMARY KEY CLUSTERED ([Roles_Id], [Users_Id] ASC);
GO

-- Creating primary key on [Teams_Id], [Users_Id] in table 'TeamEntityUserEntities'
ALTER TABLE [dbo].[TeamEntityUserEntities]
ADD CONSTRAINT [PK_TeamEntityUserEntities]
    PRIMARY KEY CLUSTERED ([Teams_Id], [Users_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ActionTypeId] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [FK_dbo_Notifications_dbo_ActionTypes_ActionTypeId]
    FOREIGN KEY ([ActionTypeId])
    REFERENCES [dbo].[ActionTypes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Notifications_dbo_ActionTypes_ActionTypeId'
CREATE INDEX [IX_FK_dbo_Notifications_dbo_ActionTypes_ActionTypeId]
ON [dbo].[Notifications]
    ([ActionTypeId]);
GO

-- Creating foreign key on [ActionTypeId] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [FK_dbo_Requests_dbo_ActionTypes_ActionTypeId]
    FOREIGN KEY ([ActionTypeId])
    REFERENCES [dbo].[ActionTypes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Requests_dbo_ActionTypes_ActionTypeId'
CREATE INDEX [IX_FK_dbo_Requests_dbo_ActionTypes_ActionTypeId]
ON [dbo].[Requests]
    ([ActionTypeId]);
GO

-- Creating foreign key on [AuthId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_dbo_Users_dbo_AuthData_AuthId]
    FOREIGN KEY ([AuthId])
    REFERENCES [dbo].[AuthDatas]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Users_dbo_AuthData_AuthId'
CREATE INDEX [IX_FK_dbo_Users_dbo_AuthData_AuthId]
ON [dbo].[Users]
    ([AuthId]);
GO

-- Creating foreign key on [IssueId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_dbo_Comments_dbo_Issues_IssueId]
    FOREIGN KEY ([IssueId])
    REFERENCES [dbo].[Issues]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Comments_dbo_Issues_IssueId'
CREATE INDEX [IX_FK_dbo_Comments_dbo_Issues_IssueId]
ON [dbo].[Comments]
    ([IssueId]);
GO

-- Creating foreign key on [UserId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_dbo_Comments_dbo_Users_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Comments_dbo_Users_UserId'
CREATE INDEX [IX_FK_dbo_Comments_dbo_Users_UserId]
ON [dbo].[Comments]
    ([UserId]);
GO

-- Creating foreign key on [Id] in table 'Features'
ALTER TABLE [dbo].[Features]
ADD CONSTRAINT [FK_dbo_Features_dbo_Teams_Id]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Teams]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TeamEntity_Id] in table 'Features'
ALTER TABLE [dbo].[Features]
ADD CONSTRAINT [FK_dbo_Features_dbo_Teams_TeamEntity_Id]
    FOREIGN KEY ([TeamEntity_Id])
    REFERENCES [dbo].[Teams]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Features_dbo_Teams_TeamEntity_Id'
CREATE INDEX [IX_FK_dbo_Features_dbo_Teams_TeamEntity_Id]
ON [dbo].[Features]
    ([TeamEntity_Id]);
GO

-- Creating foreign key on [FeatureId] in table 'Issues'
ALTER TABLE [dbo].[Issues]
ADD CONSTRAINT [FK_dbo_Issues_dbo_Features_FeatureId]
    FOREIGN KEY ([FeatureId])
    REFERENCES [dbo].[Features]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Issues_dbo_Features_FeatureId'
CREATE INDEX [IX_FK_dbo_Issues_dbo_Features_FeatureId]
ON [dbo].[Issues]
    ([FeatureId]);
GO

-- Creating foreign key on [TypeId] in table 'Issues'
ALTER TABLE [dbo].[Issues]
ADD CONSTRAINT [FK_dbo_Issues_dbo_IssueTypes_TypeId]
    FOREIGN KEY ([TypeId])
    REFERENCES [dbo].[IssueTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Issues_dbo_IssueTypes_TypeId'
CREATE INDEX [IX_FK_dbo_Issues_dbo_IssueTypes_TypeId]
ON [dbo].[Issues]
    ([TypeId]);
GO

-- Creating foreign key on [PriorityId] in table 'Issues'
ALTER TABLE [dbo].[Issues]
ADD CONSTRAINT [FK_dbo_Issues_dbo_Priorities_PriorityId]
    FOREIGN KEY ([PriorityId])
    REFERENCES [dbo].[Priorities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Issues_dbo_Priorities_PriorityId'
CREATE INDEX [IX_FK_dbo_Issues_dbo_Priorities_PriorityId]
ON [dbo].[Issues]
    ([PriorityId]);
GO

-- Creating foreign key on [ResolutionId] in table 'Issues'
ALTER TABLE [dbo].[Issues]
ADD CONSTRAINT [FK_dbo_Issues_dbo_Resolutions_ResolutionId]
    FOREIGN KEY ([ResolutionId])
    REFERENCES [dbo].[Resolutions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Issues_dbo_Resolutions_ResolutionId'
CREATE INDEX [IX_FK_dbo_Issues_dbo_Resolutions_ResolutionId]
ON [dbo].[Issues]
    ([ResolutionId]);
GO

-- Creating foreign key on [StatusId] in table 'Issues'
ALTER TABLE [dbo].[Issues]
ADD CONSTRAINT [FK_dbo_Issues_dbo_Statuses_StatusId]
    FOREIGN KEY ([StatusId])
    REFERENCES [dbo].[Statuses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Issues_dbo_Statuses_StatusId'
CREATE INDEX [IX_FK_dbo_Issues_dbo_Statuses_StatusId]
ON [dbo].[Issues]
    ([StatusId]);
GO

-- Creating foreign key on [Id] in table 'Issues'
ALTER TABLE [dbo].[Issues]
ADD CONSTRAINT [FK_dbo_Issues_dbo_Users_Id]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Leaders'
ALTER TABLE [dbo].[Leaders]
ADD CONSTRAINT [FK_dbo_Leaders_dbo_Users_Id]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [LeaderId] in table 'Teams'
ALTER TABLE [dbo].[Teams]
ADD CONSTRAINT [FK_dbo_Teams_dbo_Leaders_LeaderId]
    FOREIGN KEY ([LeaderId])
    REFERENCES [dbo].[Leaders]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Teams_dbo_Leaders_LeaderId'
CREATE INDEX [IX_FK_dbo_Teams_dbo_Leaders_LeaderId]
ON [dbo].[Teams]
    ([LeaderId]);
GO

-- Creating foreign key on [Id] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [FK_dbo_Notifications_dbo_Users_Id]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [FK_dbo_Requests_dbo_Users_Id]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Roles_Id] in table 'RoleEntityUserEntities'
ALTER TABLE [dbo].[RoleEntityUserEntities]
ADD CONSTRAINT [FK_RoleEntityUserEntities_Roles]
    FOREIGN KEY ([Roles_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_Id] in table 'RoleEntityUserEntities'
ALTER TABLE [dbo].[RoleEntityUserEntities]
ADD CONSTRAINT [FK_RoleEntityUserEntities_Users]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleEntityUserEntities_Users'
CREATE INDEX [IX_FK_RoleEntityUserEntities_Users]
ON [dbo].[RoleEntityUserEntities]
    ([Users_Id]);
GO

-- Creating foreign key on [Teams_Id] in table 'TeamEntityUserEntities'
ALTER TABLE [dbo].[TeamEntityUserEntities]
ADD CONSTRAINT [FK_TeamEntityUserEntities_Teams]
    FOREIGN KEY ([Teams_Id])
    REFERENCES [dbo].[Teams]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_Id] in table 'TeamEntityUserEntities'
ALTER TABLE [dbo].[TeamEntityUserEntities]
ADD CONSTRAINT [FK_TeamEntityUserEntities_Users]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TeamEntityUserEntities_Users'
CREATE INDEX [IX_FK_TeamEntityUserEntities_Users]
ON [dbo].[TeamEntityUserEntities]
    ([Users_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
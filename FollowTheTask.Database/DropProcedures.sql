DROP FUNCTION [dbo].[UserIdAdmin];
GO
DROP FUNCTION [dbo].[RoleIdAdmin];
GO
DROP FUNCTION [dbo].[RoleIdLeader];
GO
DROP FUNCTION [dbo].[RoleIdDeveloper];
GO
DROP FUNCTION [dbo].[RoleIdUser];
GO
DROP FUNCTION [dbo].[ActionSourceIdUserAction];
GO
DROP FUNCTION [dbo].[ActionSourceIdRequestApproved];
GO
DROP FUNCTION [dbo].[ActionSourceIdRequestDeclined];
GO
DROP FUNCTION [dbo].[ActionTypeIdBeLeader];
GO
DROP FUNCTION [dbo].[ActionTypeIdJoinTeam];
GO
DROP FUNCTION [dbo].[ActionTypeIdAssignIssue];
GO
DROP FUNCTION [dbo].[GetUserName];
GO
DROP FUNCTION [dbo].[GetRoleName];
GO
DROP FUNCTION [dbo].[GetTeamName];
GO
DROP FUNCTION [dbo].[GetFeatureName];
GO
DROP FUNCTION [dbo].[GetIssueName];
GO
DROP PROCEDURE [dbo].[SendRequest];
GO
DROP PROCEDURE [dbo].[SendNotification];
GO
DROP PROCEDURE [dbo].[SendBeLeaderRequest];
GO
DROP PROCEDURE [dbo].[SendJoinTeamRequest];
GO
DROP PROCEDURE [dbo].[SendAssignIssueRequest];
GO
DROP FUNCTION [dbo].[ResolveTarget];
GO
DROP PROCEDURE [dbo].[ExecuteRequest];
GO
DROP PROCEDURE [dbo].[ExecuteRequests];
GO
DROP PROCEDURE [dbo].[ApproveRequest];
GO
DROP PROCEDURE [dbo].[ApproveRequests];
GO
DROP PROCEDURE [dbo].[DeclineRequest];
GO
DROP PROCEDURE [dbo].[DeclineRequests];
GO
DROP PROCEDURE [dbo].[MarkNotificationRead];
GO
DROP PROCEDURE [dbo].[MarkNotificationsRead];
GO
DROP FUNCTION [dbo].[GetRoles];
GO
DROP FUNCTION [dbo].[GetTeam];
GO
DROP FUNCTION [dbo].[GetUserTeams];
GO
DROP FUNCTION [dbo].[GetLeaderTeams];
GO
DROP FUNCTION [dbo].[GetAllTeams];
GO
DROP FUNCTION [dbo].[GetTeamMembers];
GO
DROP FUNCTION [dbo].[GetFeature];
GO
DROP FUNCTION [dbo].[GetFeatures];
GO
DROP FUNCTION [dbo].[GetIssue];
GO
DROP FUNCTION [dbo].[GetIssues];
GO
DROP FUNCTION [dbo].[GetComment];
GO
DROP FUNCTION [dbo].[GetComments];
GO
DROP FUNCTION [dbo].[GetRequest];
GO
DROP FUNCTION [dbo].[GetRequests];
GO
DROP FUNCTION [dbo].[GetPendingRequests];
GO
DROP FUNCTION [dbo].[GetNotification];
GO
DROP FUNCTION [dbo].[GetNotifications];
GO
DROP FUNCTION [dbo].[CanUserCreateIssue];
GO
using System.Threading.Tasks;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Notification.ViewModels;
using FollowTheTask.TransferObjects.Notification.Commands;
using FollowTheTask.TransferObjects.Notification.DataObjects;
using FollowTheTask.TransferObjects.Notification.Queries;

namespace FollowTheTask.BLL.Services.Notification
{
    public interface INotificationService : IModelService<NotificationDto, NotificationViewModel>
    {
        QueryResult<NotificationInfoDto> GetNotificationDto(NotificationQuery query);

        Task<QueryResult<NotificationInfoDto>> GetNotificationDtoAsync(NotificationQuery query);

        QueryResult<NotificationInfoViewModel> GetNotification(NotificationQuery query);

        Task<QueryResult<NotificationInfoViewModel>> GetNotificationAsync(NotificationQuery query);

        ListQueryResult<NotificationInfoDto> GetUserNotificationsDtos(UserNotificationsQuery query);

        Task<ListQueryResult<NotificationInfoDto>> GetUserNotificationsDtosAsync(UserNotificationsQuery query);

        ListQueryResult<NotificationInfoViewModel> GetUserNotifications(UserNotificationsQuery query);

        Task<ListQueryResult<NotificationInfoViewModel>> GetUserNotificationsAsync(UserNotificationsQuery query);

        CommandResult MarkNotificationRead(NotificationReadCommand command);

        Task<CommandResult> MarkNotificationReadAsync(NotificationReadCommand command);

        CommandResult MarkNotificationsRead(NotificationsReadCommand command);

        Task<CommandResult> MarkNotificationsReadAsync(NotificationsReadCommand command);
    }
}
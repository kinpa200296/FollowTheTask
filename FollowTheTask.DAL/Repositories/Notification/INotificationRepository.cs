using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Notification.DataObjects;
using FollowTheTask.TransferObjects.Notification.Queries;

namespace FollowTheTask.DAL.Repositories.Notification
{
    public interface INotificationRepository : IModelRepository<NotificationDto>,
        IQueryRepository<NotificationQuery, NotificationInfoDto>,
        IListQueryRepository<UserNotificationsQuery, NotificationInfoDto>
    {
    }
}
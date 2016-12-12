using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Notification.DataObjects;

namespace FollowTheTask.DAL.Repositories.Notification
{
    public interface INotificationRepository : IModelRepository<NotificationDto>
    {
    }
}
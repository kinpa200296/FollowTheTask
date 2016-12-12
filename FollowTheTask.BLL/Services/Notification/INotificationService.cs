using FollowTheTask.BLL.Services.Model;
using FollowTheTask.BLL.Services.Notification.ViewModels;
using FollowTheTask.TransferObjects.Notification.DataObjects;

namespace FollowTheTask.BLL.Services.Notification
{
    public interface INotificationService : IModelService<NotificationDto, NotificationViewModel>
    {
    }
}
using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Notification.DataObjects;

namespace FollowTheTask.DAL.Repositories.Notification
{
    public class NotificationRepository : ModelRepository<NotificationModel, NotificationDto>, INotificationRepository
    {
        public NotificationRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}
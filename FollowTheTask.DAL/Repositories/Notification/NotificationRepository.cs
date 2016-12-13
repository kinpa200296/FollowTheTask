using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Notification.DataObjects;
using FollowTheTask.TransferObjects.Notification.Queries;

namespace FollowTheTask.DAL.Repositories.Notification
{
    public class NotificationRepository : ModelRepository<NotificationModel, NotificationDto>, INotificationRepository
    {
        public NotificationRepository(FollowTheTaskContext context) : base(context)
        {
        }


        #region Queries Implementation

        public NotificationInfoDto Handle(NotificationQuery query)
        {
            return
                Context.Database.SqlQuery<NotificationInfoDto>("select * from [dbo].GetNotification(@NotificationId)",
                    new SqlParameter("NotificationId", query.Id)).FirstOrDefault();
        }

        public async Task<NotificationInfoDto> HandleAsync(NotificationQuery query)
        {
            return
                await Context.Database.SqlQuery<NotificationInfoDto>("select * from [dbo].GetNotification(@NotificationId)",
                    new SqlParameter("NotificationId", query.Id)).FirstOrDefaultAsync();
        }

        public IQueryable<NotificationInfoDto> Handle(UserNotificationsQuery query)
        {
            return
                Context.Database.SqlQuery<NotificationInfoDto>("select * from [dbo].GetNotifications(@UserId)",
                    new SqlParameter("UserId", query.UserId)).AsQueryable();
        }

        public Task<IQueryable<NotificationInfoDto>> HandleAsync(UserNotificationsQuery query)
        {
            return
                Task.FromResult(
                    Context.Database.SqlQuery<NotificationInfoDto>("select * from [dbo].GetNotifications(@UserId)",
                        new SqlParameter("UserId", query.UserId)).AsQueryable());
        }

        #endregion Queries Implementation


        #region Commands Implementation

        #endregion Commands Implementation
    }
}
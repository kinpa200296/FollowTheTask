using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FollowTheTask.DAL.Contexts;
using FollowTheTask.DAL.Models;
using FollowTheTask.DAL.Repositories.Model;
using FollowTheTask.TransferObjects.Notification.Commands;
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

        public void Execute(NotificationReadCommand command)
        {
            Context.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction,
                "exec [dbo].MarkNotificationRead @NotificationId", new SqlParameter("NotificationId", command.NotificationId));
        }

        public async Task ExecuteAsync(NotificationReadCommand command)
        {
            await
                Context.Database.ExecuteSqlCommandAsync(TransactionalBehavior.EnsureTransaction,
                    "exec [dbo].MarkNotificationRead @NotificationId", new SqlParameter("NotificationId", command.NotificationId));
        }

        public void Execute(NotificationsReadCommand command)
        {
            Context.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction,
                "exec [dbo].MarkNotificationsRead @UserId", new SqlParameter("UserId", command.UserId));
        }

        public async Task ExecuteAsync(NotificationsReadCommand command)
        {
            await
                Context.Database.ExecuteSqlCommandAsync(TransactionalBehavior.EnsureTransaction,
                    "exec [dbo].MarkNotificationsRead @UserId", new SqlParameter("UserId", command.UserId));
        }

        #endregion Commands Implementation
    }
}
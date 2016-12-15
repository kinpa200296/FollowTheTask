using System.Linq;
using System.Web.Mvc;
using FollowTheTask.BLL.Services.Notification;
using FollowTheTask.TransferObjects.Notification.Commands;
using FollowTheTask.TransferObjects.Notification.Queries;
using Microsoft.AspNet.Identity;

namespace FollowTheTask.Web.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;


        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var request = _notificationService.GetUserNotifications(
                new UserNotificationsQuery {UserId = int.Parse(User.Identity.GetUserId())});
            if (request.IsFailed) return RedirectToAction("Index", "Error", new {message = request.Message});
            if (request.Value == null) return RedirectToAction("Index", "Error");
            return View(request.Value.ToArray());
        }

        public ActionResult MarkAsSeen(int id)
        {
            var request = _notificationService.GetNotification(new NotificationQuery {Id = id});
            if (request.IsFailed) return RedirectToAction("Index", "Error", new {message = request.Message});
            if (request.Value == null) return RedirectToAction("NotFound", "Error");
            if (request.Value.ReceiverId.ToString() != User.Identity.GetUserId())
                return RedirectToAction("Unauthorized", "Error");
            var command = _notificationService.MarkNotificationRead(
                new NotificationReadCommand {NotificationId = id});
            if (command.IsFailed) return RedirectToAction("Index", "Error", new {message = command.Message});
            return RedirectToAction("Index");
        }

        public ActionResult MarkAllAsSeen()
        {
            var command = _notificationService.MarkNotificationsRead(
                new NotificationsReadCommand {UserId = int.Parse(User.Identity.GetUserId())});
            if (command.IsFailed) return RedirectToAction("Index", "Error", new {message = command.Message});
            return RedirectToAction("Index");
        }
    }
}
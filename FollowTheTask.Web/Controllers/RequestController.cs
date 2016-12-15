using System.Linq;
using System.Web.Mvc;
using FollowTheTask.BLL.Services.Request;
using FollowTheTask.TransferObjects.Request.Commands;
using FollowTheTask.TransferObjects.Request.Queries;
using Microsoft.AspNet.Identity;

namespace FollowTheTask.Web.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;


        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var request = _requestService.GetUserPendingRequests(
                new UserPendingRequestsQuery {UserId = int.Parse(User.Identity.GetUserId())});
            if (request.IsFailed) return RedirectToAction("Index", "Error", new {message = request.Message});
            if (request.Value == null) return RedirectToAction("Index", "Error");
            return View(request.Value.ToArray());
        }

        public ActionResult Approve(int id)
        {
            var request = _requestService.GetRequest(new RequestQuery {Id = id});
            if (request.IsFailed) return RedirectToAction("Index", "Error", new {message = request.Message});
            if (request.Value == null) return RedirectToAction("NotFound", "Error");
            if (request.Value.ReceiverId.ToString() != User.Identity.GetUserId())
                return RedirectToAction("Unauthorized", "Error");
            var command = _requestService.ApproveRequest(new ApproveRequestCommand {RequestId = id});
            if (command.IsFailed) return RedirectToAction("Index", "Error", new {message = command.Message});
            return RedirectToAction("Index");
        }

        public ActionResult Decline(int id)
        {
            var request = _requestService.GetRequest(new RequestQuery {Id = id});
            if (request.IsFailed) return RedirectToAction("Index", "Error", new {message = request.Message});
            if (request.Value == null) return RedirectToAction("NotFound", "Error");
            if (request.Value.ReceiverId.ToString() != User.Identity.GetUserId())
                return RedirectToAction("Unauthorized", "Error");
            var command = _requestService.DeclineRequest(new DeclineRequestCommand {RequestId = id});
            if (command.IsFailed) return RedirectToAction("Index", "Error", new {message = command.Message});
            return RedirectToAction("Index");
        }

        public ActionResult ApproveAll()
        {
            var command = _requestService.ApprovePendingRequests(
                new ApproveUserRequestsCommand {UserId = int.Parse(User.Identity.GetUserId())});
            if (command.IsFailed) return RedirectToAction("Index", "Error", new {message = command.Message});
            return RedirectToAction("Index");
        }

        public ActionResult DeclineAll()
        {
            var command = _requestService.DeclinePendingRequests(
                new DeclineUserRequestsCommand {UserId = int.Parse(User.Identity.GetUserId())});
            if (command.IsFailed) return RedirectToAction("Index", "Error", new {message = command.Message});
            return RedirectToAction("Index");
        }
    }
}
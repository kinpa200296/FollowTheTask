using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FollowTheTask.BLL.Services.Issue;
using FollowTheTask.BLL.Services.Issue.ViewModels;
using FollowTheTask.TransferObjects.Issue.Queries;
using Microsoft.AspNet.Identity;

namespace FollowTheTask.Web.Controllers
{
    [Authorize]
    public class IssueController : Controller
    {
        private readonly IIssueService _issueService;

        public IssueController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public ActionResult Index(int id)
        {
            var request = _issueService.GetIssue(new IssueQuery {Id = id});
            if (request.IsFailed) return RedirectToAction("Index", "Error", request.Message);
            if (request.Value == null) RedirectToAction("Index", "Error");
            return View(request.Value);
        }

        [HttpGet]
        public ActionResult Create(int featureId)
        {
            return View(new IssueInfoViewModel() {FeatureId = featureId});
        }

        [HttpPost]
        public ActionResult Create(IssueViewModel model)
        {
            if (model == null) return RedirectToAction("Index", "Error");

            //needed? other fields?
            model.ReporterId = int.Parse(User.Identity.GetUserId());

            var queryResult = _issueService.CreateModel(model);
            if (queryResult.IsFailed) return RedirectToAction("Internal", "Error");
            return Index(model.Id);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _issueService.GetIssue(new IssueQuery { Id = id });
            if (model != null) return View(model);
            return RedirectToAction("Internal", "Error");
        }

        [HttpPost]
        public ActionResult Edit(IssueViewModel model)
        {
            if (model == null) return RedirectToAction("Index", "Error");
            //check access violation here

            var requestResult = _issueService.UpdateModel(model);

            if (requestResult.IsFailed) return RedirectToAction("Index", "Error", requestResult.Message);
            return Index(model.Id);            
        }

        [Authorize(Roles = "Admin,Leader")]
        public ActionResult Delete(int id)
        {
            var result = _issueService.DeleteModel(id);
            if (result.IsFailed) return RedirectToAction("Index", "Error", result.Message);
            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public ActionResult GetComments(int issueId)
        {
            var list = _issueService.GetIssueComments(new IssueCommentsQuery {IssueId = issueId}).Value?.ToArray();
            return PartialView(list);
        }
    }
}
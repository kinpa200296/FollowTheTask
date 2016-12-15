using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
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
            if (request.IsFailed) return RedirectToAction("Index", "Error", new {message = request.Message});
            if (request.Value == null) return RedirectToAction("Index", "Error");
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

            model.AssigneeId = int.Parse(User.Identity.GetUserId());
            model.ReporterId = int.Parse(User.Identity.GetUserId());
            model.CreatedDateUtc = DateTimeOffset.UtcNow;

            var queryResult = _issueService.CreateModel(model);
            if (queryResult.IsFailed) return RedirectToAction("Internal", "Error");
            return RedirectToAction("Index", new { id = model.Id });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _issueService.GetIssue(new IssueQuery { Id = id });
            if (model.Value != null) return View(model.Value);
            return RedirectToAction("Internal", "Error");
        }

        [HttpPost]
        public ActionResult Edit(IssueInfoViewModel model)
        {
            if (model == null) return RedirectToAction("Index", "Error");
            //check access violation here

            var requestResult = _issueService.UpdateModel(Mapper.Map<IssueViewModel>(model));

            if (requestResult.IsFailed) return RedirectToAction("Index", "Error", new { message = requestResult.Message});
            return RedirectToAction("Index", new { id = model.Id });            
        }

        [Authorize(Roles = "Admin,Leader")]
        public ActionResult Delete(int id)
        {
            var result = _issueService.DeleteModel(id);
            if (result.IsFailed) return RedirectToAction("Index", "Error", new { message = result.Message});
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
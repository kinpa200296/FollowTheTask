﻿using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FollowTheTask.BLL.Services.Issue;
using FollowTheTask.BLL.Services.Issue.ViewModels;
using FollowTheTask.TransferObjects.Issue.Commands;
using FollowTheTask.TransferObjects.Issue.Queries;
using FollowTheTask.TransferObjects.Team.Commands;
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
            return View(new IssueViewModel() {FeatureId = featureId});
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
            var featureId = _issueService.GetIssue(new IssueQuery() {Id = id}).Value?.FeatureId;
            var result = _issueService.DeleteModel(id);
            if (result.IsFailed) return RedirectToAction("Index", "Error", new { message = result.Message});
            if (featureId == null) return RedirectToAction("Index", "Error");
            return RedirectToAction("Index", "Feature", new {id = featureId});
        }

        public ActionResult AssignToMe(int issueId)
        {
            var command = _issueService.AssignIssue(
                new RequestAssignIssueCommand {UserId = int.Parse(User.Identity.GetUserId()), IssueId = issueId});
            if (command.IsFailed) return RedirectToAction("Index", "Error", new {message = command.Message});
            return RedirectToAction("Index", new { id = issueId });
        }

        [ChildActionOnly]
        public ActionResult GetComments(int issueId)
        {
            var list = _issueService.GetIssueComments(new IssueCommentsQuery {IssueId = issueId}).Value?.ToArray();
            return PartialView(list);
        }
    }
}
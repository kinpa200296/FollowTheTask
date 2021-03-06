﻿using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FollowTheTask.BLL.Services.Feature;
using FollowTheTask.BLL.Services.Feature.ViewModels;
using FollowTheTask.BLL.Services.Team;
using FollowTheTask.TransferObjects.Feature.Queries;
using FollowTheTask.TransferObjects.Team.Queries;
using Microsoft.AspNet.Identity;

namespace FollowTheTask.Web.Controllers
{
    [Authorize]
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        public ActionResult Index(int id)
        {

            var request = _featureService.GetFeature(new FeatureQuery() {Id = id});
            if (request.IsFailed) return RedirectToAction("Index", "Error", new { message = request.Message});
            if (request.Value == null) return RedirectToAction("Index", "Error");
            return View(request.Value);
        }

        [HttpGet]
        public ActionResult Create(int teamId)
        {
            return View(new FeatureViewModel {TeamId = teamId});
        }

        [HttpPost]
        public ActionResult Create(FeatureViewModel model)
        {
            if (model == null) return RedirectToAction("Index", "Error");

            var result = _featureService.CreateModel(model);

            if (result.IsFailed) return RedirectToAction("Internal", "Error");
            return RedirectToAction("Index", new {id = model.Id});
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _featureService.GetFeature(new FeatureQuery {Id = id});
            if (model.Value != null) return View(model.Value);
            return RedirectToAction("Internal", "Error");
        }

        [HttpPost]
        public ActionResult Edit(FeatureInfoViewModel model)
        {
            if (model == null) return RedirectToAction("Index", "Error");
            
            //this is bit unsafe
            var leaderId = DependencyResolver.Current.GetService<ITeamService>().GetTeam(new TeamQuery {Id = model.TeamId}).Value?.LeaderId;
            if (!(leaderId == int.Parse(User.Identity.GetUserId()) || User.IsInRole("Admin")))
                RedirectToAction("AccessViolation", "Error");

            var requestResult = _featureService.UpdateModel(Mapper.Map<FeatureViewModel>(model));

            if (requestResult.IsFailed) return RedirectToAction("Index", "Error", new { message = requestResult.Message});
            return RedirectToAction("Index", new { id = model.Id });
        }

        [Authorize(Roles = "Admin,Leader")]
        public ActionResult Delete(int id)
        {
            //this is bit unsafe, check if user is teamLeader
            var teamId = _featureService.GetFeature(new FeatureQuery {Id = id}).Value.TeamId;
            if (!User.IsInRole("Admin"))
            {
                var leaderId = DependencyResolver.Current.GetService<ITeamService>().GetTeam(new TeamQuery { Id = teamId }).Value?.LeaderId;
                if (leaderId != int.Parse(User.Identity.GetUserId())) return RedirectToAction("AccessViolation", "Error");
            }
            
            var result = _featureService.DeleteModel(id);
            if (result.IsFailed) return RedirectToAction("Index", "Error", new {message = result.Message});
            return RedirectToAction("Index", "Team", new {id = teamId});
        }

        [ChildActionOnly]
        public ActionResult GetFeatureIssues(int featureId)
        {
            var list =
                _featureService.GetFeatureIssues(new FeatureIssuesQuery() {FeatureId = featureId}).Value?.ToArray();
            return PartialView(list);
        }
    }
}
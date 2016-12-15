using System;
using System.Web.Mvc;
using AutoMapper;
using FollowTheTask.BLL.Services.Comment;
using FollowTheTask.BLL.Services.Comment.ViewModels;
using FollowTheTask.TransferObjects.Comment.Queries;
using Microsoft.AspNet.Identity;

namespace FollowTheTask.Web.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        //public ActionResult Index(int id)
        //{
        //    var comment = _commentService.GetComment(new CommentQuery {Id = id}).Value;
        //    if (comment == null) return RedirectToAction("Internal", "Error");
        //    return PartialView(comment);
        //}

        public ActionResult Index(CommentInfoViewModel model)
        {
            return PartialView(model);
        }


        public ActionResult Create(int issueId)
        {
            return PartialView(new CommentViewModel {IssueId = issueId});
        }

        [HttpPost]
        public ActionResult Create(CommentViewModel model)
        {
            if (model == null) return RedirectToAction("Index", "Error");

            model.DateCreatedUtc = DateTimeOffset.UtcNow;
            model.UserId = int.Parse(User.Identity.GetUserId());

            var request =_commentService.CreateModel(model);
            if (request.IsFailed) return RedirectToAction("Internal", "Error");
            return RedirectToAction("Index", "Issue", new {id = model.IssueId});
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _commentService.GetComment(new CommentQuery { Id = id });
            if (model.Value != null) return View(model.Value);
            return RedirectToAction("Internal", "Error");
        }

        [HttpPost]
        public ActionResult Edit(CommentInfoViewModel model)
        {
            if (model == null) return RedirectToAction("Index", "Error");

            var request =_commentService.UpdateModel(Mapper.Map<CommentViewModel>(model));
            if (request.IsFailed) return RedirectToAction("Internal", "Error");

            return RedirectToAction("Index", "Issue", new {id = model.IssueId});
        }

        public ActionResult Delete(int id)
        {
            var issueId = _commentService.GetComment(new CommentQuery() {Id = id}).Value?.IssueId;
            var request = _commentService.DeleteModel(id);
            if (request.IsFailed) return RedirectToAction("Index", "Error", new {message = request.Message});
            if (issueId == null) return RedirectToAction("Index", "Error");
            return RedirectToAction("Index", "Issue", new {id = issueId});
        }
    }
}

using System;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Comment.ViewModels
{
    public class CommentInfoViewModel : ModelViewModel
    {
        public string Message { get; set; }

        public DateTimeOffset DateCreatedUtc { get; set; }

        public string UserName { get; set; }

        public int? UserId { get; set; }

        public string User { get; set; }

        public int IssueId { get; set; }

        public string Issue { get; set; }
    }
}
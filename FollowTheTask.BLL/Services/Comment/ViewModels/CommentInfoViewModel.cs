using System;
using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Comment.ViewModels
{
    public class CommentInfoViewModel : ModelViewModel
    {
        [Required]
        [Display(Name = "Message")]
        [StringLength(700)]
        public string Message { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset DateCreatedUtc { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }

        public int? UserId { get; set; }

        [Display(Name = "User")]
        public string User { get; set; }

        public int IssueId { get; set; }

        [Display(Name = "Issue")]
        public string Issue { get; set; }
    }
}
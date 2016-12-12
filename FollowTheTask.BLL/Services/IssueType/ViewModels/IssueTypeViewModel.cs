using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.IssueType.ViewModels
{
    public class IssueTypeViewModel : ModelViewModel
    {
        [Display(Name = "Issue type")]
        [StringLength(70)]
        public string Name { get; set; }
    }
}
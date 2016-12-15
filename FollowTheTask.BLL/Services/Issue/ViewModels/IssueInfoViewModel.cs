using System;
using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Issue.ViewModels
{
    public class IssueInfoViewModel : ModelViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(3000)]
        public string Description { get; set; }

        [Display(Name = "Version")]
        [StringLength(50)]
        public string Version { get; set; }

        [Display(Name = "Type")]
        public int? TypeId { get; set; }

        [Display(Name = "Priority")]
        public int? PriorityId { get; set; }

        [Display(Name = "Status")]
        public int? StatusId { get; set; }

        [Display(Name = "Resolution")]
        public int? ResolutionId { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedDateUtc { get; set; }

        [Display(Name = "Deadline")]
        public DateTimeOffset? DeadlineDateUtc { get; set; }

        public int FeatureId { get; set; }

        [Display(Name = "Feature")]
        [StringLength(100)]
        public string Feature { get; set; }

        [Display(Name = "Reporter")]
        [StringLength(120)]
        public string ReporterName { get; set; }

        public int? ReporterId { get; set; }

        [Display(Name = "Reporter")]
        [StringLength(120)]
        public string Reporter { get; set; }

        [Display(Name = "Assignee")]
        [StringLength(120)]
        public string AssigneeName { get; set; }

        public int? AssigneeId { get; set; }

        [Display(Name = "Assignee")]
        [StringLength(120)]
        public string Assignee { get; set; }
    }
}
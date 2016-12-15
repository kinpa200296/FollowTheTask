using System;
using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Issue.ViewModels
{
    public class IssueViewModel : ModelViewModel
    {
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

        public string ReporterName { get; set; }

        [Display(Name = "Reporter")]
        public int? ReporterId { get; set; }

        public string AssigneeName { get; set; }

        [Display(Name = "Assignee")]
        public int? AssigneeId { get; set; }
    }
}
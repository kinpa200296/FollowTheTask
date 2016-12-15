using System;
using System.ComponentModel.DataAnnotations;
using FollowTheTask.BLL.Services.Model.ViewModels;

namespace FollowTheTask.BLL.Services.Auth.ViewModels
{
    public class AuthViewModel : ModelViewModel
    {
        [Display(Name = "Lockout Enabled")]
        public bool LockoutEnabled { get; set; }

        [Display(Name = "Access Failed Count")]
        public int AccessFailedCnt { get; set; }

        [Display(Name = "Lockout End Date")]
        public DateTimeOffset? LockoutDateUtc { get; set; }

        [Display(Name = "Total Access Granted Count")]
        public int AccessGrantedTotal { get; set; }

        [Display(Name = "Last Access Granted Date")]
        public DateTimeOffset? LastAccessGrantedDateUtc { get; set; }

        [Display(Name = "Total Access Failed Count")]
        public int AccessFailedTotal { get; set; }

        [Display(Name = "Last Access Failed Date")]
        public DateTimeOffset? LastAccessFailedDateUtc { get; set; }
    }
}
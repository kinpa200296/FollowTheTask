﻿using System.ComponentModel.DataAnnotations;

namespace FollowTheTask.BLL.Services.User.ViewModels
{
    public class LoginViewModel : ViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
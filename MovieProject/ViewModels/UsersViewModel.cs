using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieProject.ViewModels
{
    public class UsersViewModel
    {
        [Required (ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Please specify a valid Email address")]
        [Display(Name = "Login:")]
        [StringLength(100)]
        public string Login { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Role:")]
        public string Role { get; set; }

    }
}
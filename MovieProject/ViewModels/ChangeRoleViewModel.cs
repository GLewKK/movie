using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieProject.ViewModels
{
    public class ChangeRoleViewModel
    {

        public Guid DataExtraId { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}
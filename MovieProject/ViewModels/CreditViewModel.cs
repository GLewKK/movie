using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieProject.ViewModels
{
    public class CreditViewModel
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name = "IDNP")]
        public string IDNP { get; set; }

        [Required]
        [Display(Name = "Person")]
        public bool PersonType { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Required]  
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Credit Sum")]
        public decimal Sum { get; set; }

        [Required]
        [Display(Name = "Birthday Date")]
        public DateTime DateOfBirth { get; set; }

        public string DateOfBirthString { get; set; }

        [Required]
        [Display(Name = "WorkSpace")]
        public string WorkSpace { get; set; }

        [Required]
        [Phone]
        [Display(Name = "WorkSpace Phone")]
        public string TelephoneWorkSpace { get; set; }

        [Required]
        [Display(Name = "Official Venit")]
        public decimal OfficialVenit { get; set; }

        [Required]
        [Display(Name = "Married")]
        public bool IsMarried { get; set; }

        [Display(Name = "WorkSpace Husband Wife")]
        public string WorkSpaceHusbandWife { get; set; }

        [Display(Name = "Official Venit")]
        public decimal OfficialVenitHusbandWife { get; set; }

        [Display(Name = "Months")]
        public int Months { get; set; }
    }
}
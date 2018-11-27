using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DAL.Models;

namespace MovieProject.ViewModels
{
    public class RequestFormViewModel
    {
        public Request Request { get; set; }
        public FormErrors FormErrors { get; set; }
        public List<string> ListForms { get; set; }
        public string[] ArrayStrings { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IDNP { get; set; }
        public string Telephone { get; set; }
        public string DateofBirthString { get; set; }
        public string WorkSpace { get; set; }
        public string TelephoneWorkSpace { get; set; }
        public decimal OfficialVenit { get; set; }
        public bool IsMarried { get; set; }
        public string WorkSpaceHusbandWife { get; set; }
        public decimal OfficialVenitHusbandWife { get; set; }
        public Guid RequestId { get; set; }

    }
}
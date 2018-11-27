using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DataExtra
    {
        [Key]

        public Guid Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }

        [MaxLength(50)  ]
        public string Telephone { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public bool? PersonType { get; set; }

        [MaxLength (20)]
        public string IDNP { get; set; }

        public virtual Image Images { get; set; }

        public Guid ImagesId { get; set; }

        public DateTime CreationDate { get; set; }

        [MaxLength(20)]
        public string Role { get; set; }


        public DateTime? DateOfBirth { get; set; }

        [MaxLength(50)]
        public string WorkSpace { get; set; }

        [MaxLength(20)]
        public string TelephoneWorkSpace { get; set; }

        public decimal OfficialVenit { get; set; }

        public bool IsMarried { get; set; }

        [MaxLength(50)]
        public string WorkSpaceHusbandWife { get; set; }

        public decimal OfficialVenitHusbandWife { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Credit
    {
        [Key]
        public Guid Id { get; set; }

        public virtual DataExtra DataExtra { get; set; }

        public Guid DataExtraId { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsDeleted { get; set; }

        public decimal Sum { get; set; }

        public int Months { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class FormErrors
    {
        [Key]
        public int Id { get; set; }

        public virtual Request Request { get; set; }

        public Guid RequestId { get; set; }

        public string FormId { get; set; }

    }
}

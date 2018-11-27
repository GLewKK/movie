using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Telephone
    {
        public Guid Id { get; set; }

        public string CountryName { get; set; }

        public string NumberCode { get; set; }

        public string NumberExample { get; set; }

    }
}

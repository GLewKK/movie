using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Request
    {
        public Guid Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public string CreditorId { get; set; }

        public string ModeratorId { get; set; }

        public virtual Status Status { get; set; }

        public Guid StatusId { get; set; }

        public virtual Credit Credit { get; set; }

        public Guid CreditId { get; set; }

        public bool? IsAccepted { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool Seen { get; set; }


    }
}

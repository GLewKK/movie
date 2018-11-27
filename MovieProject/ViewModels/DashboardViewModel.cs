using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Models;

namespace MovieProject.ViewModels
{
    public class DashboardViewModel
    {
        public List<Request> AllRequests { get; set; }

        public IEnumerable<DataExtra> AllUsers { get; set; }

        public int CancelledRequests { get; set; }

        public int RejectedRequests { get; set; }

        public int ApprovedRequests { get; set; }

        public int InProgressRequests { get; set; }
    }
}
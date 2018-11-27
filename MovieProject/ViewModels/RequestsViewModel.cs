using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Models;

namespace MovieProject.ViewModels
{
    public class RequestsViewModel
    {
        public List<Request> MyRequests { get; set; }

        public List<Request> AllRequests { get; set; }

        public List<Request> AllRequestsUserNames { get; set; }

        public List<Request> CompletedRequests { get; set; }

        public int PageType { get; set; }


    }
}
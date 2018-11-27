using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL;
using DAL.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using MovieProject.Models;
using MovieProject.ViewModels;
using UserRoles = MovieProject.ViewModels.UserRoles;

namespace MovieProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITakeRequest iRequest = new TakeRequest();
        private readonly ApplicationUserManager _userManager;
        public ActionResult Index()
        {
            if (HttpContext.User.IsInRole(UserRoles.Editor)||HttpContext.User.IsInRole(UserRoles.Moderator))
            {
                return View("Index_Editor");
            }

            else if (HttpContext.User.IsInRole(UserRoles.User))
            {
                ITakeRequest iRequest = new TakeRequest();
                var id = User.Identity.GetUserId();
                bool hasCredit = iRequest.CheckCredit(id);
                if (hasCredit)
                {
                    return RedirectToAction("Request");
                }else
                {
                    return View("Index_User");
                }
            }
            else if (!HttpContext.User.Identity.IsAuthenticated)
            {
            return View("Index_Guest");
            }

            var dashboard = new DashboardViewModel()
            {
                AllRequests=iRequest.GetNotificationList(UserRoles.Admin),
                AllUsers= iRequest.GetDataExtras(),
                CancelledRequests = iRequest.AdminRequests("cancelled", null, null),
                RejectedRequests = iRequest.AdminRequests("rejected", null, null),
                ApprovedRequests = iRequest.AdminRequests("approved", null, null),
                InProgressRequests = iRequest.AdminRequests("pending", null, null)
            };

            return View(dashboard);

        }

        [Authorize(Roles =UserRoles.Admin)]
        public ActionResult AllRequests()
        {
            var request = new RequestsViewModel()
            {
                AllRequests = iRequest.GetNotificationList(UserRoles.Admin),
                MyRequests = iRequest.GetMyRequests(null, false, UserRoles.Admin),
                CompletedRequests = iRequest.GetMyRequests(null, true, UserRoles.Admin)
            };
            return View("AllRequests", request);
        }

        public ActionResult Take(Guid id)
        {
            var userId = HttpContext.User.Identity.GetUserId();
            iRequest.SetCreditorModeratorId(userId,id);

            return RedirectToAction("Request");
        }

        public ActionResult Tasks()
        {
            var id = HttpContext.User.Identity.GetUserId();
            if (HttpContext.User.IsInRole(UserRoles.Editor))
            {
                var request = new RequestsViewModel()
                {
                    AllRequests = iRequest.GetNotificationList(UserRoles.Editor),
                    MyRequests = iRequest.GetMyRequests(id, false, UserRoles.Editor),
                    CompletedRequests = iRequest.GetMyRequests(id, true, UserRoles.Editor)

                };

                return View("Tasks_Editor", request);
            }

            if (HttpContext.User.IsInRole(UserRoles.Moderator))
            {
                var request = new RequestsViewModel()
                {
                    AllRequests=iRequest.GetNotificationList(UserRoles.Moderator),
                    MyRequests= iRequest.GetMyRequests(id, false, UserRoles.Moderator),
                    CompletedRequests = iRequest.GetMyRequests(id, true, UserRoles.Moderator)
                };
                return View("Tasks_Moderator",request);
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ApplicationDbContext context = new ApplicationDbContext();
            var users = context.Users.ToList();

            return View();
        }
        public ActionResult Request()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var request = iRequest.GetRequest(userId);
            var dataErrors = new FormErrors();
            if (request != null)
            {
                dataErrors = iRequest.FindFormModelErrors(request.Id);

            }
            else
            {
                return RedirectToAction("Index");
            }
            var model = new RequestFormViewModel();
            if (dataErrors != null)
            {
                model.ListForms = dataErrors.FormId.Split(',').ToList();
                model.ArrayStrings = new[] {dataErrors.FormId};
            }

                model.Request = request;

            if (HttpContext.User.IsInRole(UserRoles.Editor))
            {
                return View("Request_Editor", model.Request);
            }

            if (HttpContext.User.IsInRole(UserRoles.Moderator))
            {
                return View("Request_Moderator", model.Request); 
            }
                return View("Request_User",model);
        }

    }
}
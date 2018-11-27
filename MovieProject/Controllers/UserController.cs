using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MovieProject.ViewModels;

namespace MovieProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private readonly ITakeRequest iRequest = new TakeRequest();

        private ApplicationUserManager _userManager;
        //private readonly ApplicationDbContext _context = new ApplicationDbContext();
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            // IEnumerable<DAL.Models.DataExtra> users = _context.DataExtra.ToList();
            IEnumerable<DataExtra> users = iRequest.GetDataExtras();

            return View(users);

        }

        public ActionResult Details(Guid id)
        {
            var model = iRequest.GetDataExtras(id);
            return View(model);
        }

        
        public ActionResult ChangeRole(Guid dataExtraId)
        {
             var result = iRequest.GetDataExtras(dataExtraId);

            if (result.UserId == HttpContext.User.Identity.GetUserId())
            {
                return PartialView("_ChangeRole");
            }
            else
            {
                return PartialView("_ChangeRole",result);
            }
        }
        public string SetRole(string userId, string role)
        {

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var _context = new ApplicationDbContext();
            var user = manager.FindById(userId);
            var oldRoleId = user.Roles.SingleOrDefault().RoleId;
            var oldRoleName = _context.Roles.FirstOrDefault(x => x.Id == oldRoleId).Name;
            
            if (oldRoleName != role)
            {

                iRequest.SetCreditorIdToNull(userId, oldRoleName);

                manager.RemoveFromRole(user.Id, oldRoleName);
                manager.AddToRole(user.Id, role);

                iRequest.SetRoleByUserId(userId, role);


                return "Success";
            }
            return "You select same role";
           
        }

            //[HttpPost]
            //public ActionResult ChangeRole(UsersViewModel model)
            //{

            //    return PartialView("ChangeRole");
            //}

            public ActionResult Create()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create(UsersViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {UserName = model.Login, Email = model.Login};
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
                    var roleManager = new RoleManager<IdentityRole>(roleStore);
                    await roleManager.CreateAsync(new IdentityRole(model.Role));
                    await UserManager.AddToRoleAsync(user.Id, model.Role);
                }

                iRequest.SetDataExtra(user.Id, model.Role);

                return Json(new
                {
                    success=true
                });
            }

            return Json(new { success = false });



        }

        private ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            set => _userManager = value;
        }

        [HttpPost]
        public string SetStatus(string[] list, Guid id, string status)
        {

            if (list == null)
            {
                switch (status)
                {
                    case DbStatus.EditorToManager:
                    {
                        var statusId = iRequest.GetStatusByString(status);
                        iRequest.ChangeStatus(id, statusId);
                        return "Success";
                    }
                    case DbStatus.ManagerToUserGood:
                    {
                        var statusId = iRequest.GetStatusByString(status);
                        iRequest.ChangeStatus(id, statusId);
                        iRequest.SetRequestResult(id, true);
                        return "Success";
                    }
                    case DbStatus.ManagerToUserBad:
                    {
                        var statusId = iRequest.GetStatusByString(status);
                        iRequest.ChangeStatus(id, statusId);
                        iRequest.SetRequestResult(id, false);
                        return "Success";
                    }
                    case DbStatus.Cancelled:
                    {
                        var statusId = iRequest.GetStatusByString(status);
                        iRequest.ChangeStatus(id, statusId);
                        iRequest.SetRequestResult(id, false);
                        return "Success";
                    }
                    default:
                        return "An error occured! Status is not matching123";
                }
            }
            else
            {

                if (status == DbStatus.EditorToUser)
                {
                    var statusId = iRequest.GetStatusByString(status);

                    iRequest.ChangeStatus(id, statusId);
                    iRequest.SetErrorForms(id, list, statusId);
                    return "Success";
                }
                else
                {
                    return "An error occured! Status is not matching";
                }

            }
        }

        [HttpPost]
        public JsonResult SetStatusToEditor(RequestFormViewModel model)
        {
            if (model == null)
            {
                return Json("!!!", JsonRequestBehavior.AllowGet);
            }

            var request = new Request
                {   
                    Credit = new Credit()
                    {
                        DataExtra = new DataExtra()
                    },
                    Id = model.RequestId
                };
            if (ModelState.IsValid)
            { 
                var dataextra = new DataExtra();
                dataextra.FirstName = model.FirstName;
                dataextra.LastName = model.LastName;
                dataextra.Telephone = model.Telephone;
                dataextra.IDNP = model.IDNP;
                dataextra.DateOfBirth = DateTime.Parse(model.DateofBirthString);
                dataextra.IsMarried = model.IsMarried;
                dataextra.WorkSpace = model.WorkSpace;
                dataextra.WorkSpaceHusbandWife = model.WorkSpaceHusbandWife;
                dataextra.TelephoneWorkSpace = model.TelephoneWorkSpace;
                dataextra.OfficialVenitHusbandWife = model.OfficialVenitHusbandWife;

            request.Credit.DataExtra = dataextra;

            var statusId = iRequest.GetStatusByString(DbStatus.UserToEditor);
            iRequest.ChangeStatus(request, statusId);

            return new JsonResult {Data = "Succesfully changed", JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            }

            return new JsonResult { Data = "Not changed", JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

        public JsonResult GetRequestJsonResult()
        {
            var userId1 = HttpContext.User.Identity.GetUserId();
            var model = iRequest.GetRequest(userId1);

            return new JsonResult {Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        [HttpPost]
        public JsonResult GetFinishedRequests(string userId)
        {
            var dateTimeList = iRequest.GetCountCompletedRequest(userId);

            return new JsonResult { Data = dateTimeList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public string GetCurrentStatus(Guid requestId)
        {
            var result = iRequest.GetRequestStatus(requestId);
            return result;

        }
        public JsonResult GetErrorList(Guid requestId)
        {
            var model = iRequest.FindFormModelErrors(requestId);
            //var result = model.FormId.Split(',').ToList();
            var result = model.FormId;
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public PartialViewResult GetRequestChart(string userId, string role)
        {
            var result = new DashboardViewModel()
            {
                CancelledRequests = iRequest.AdminRequests("cancelled", userId, role),
                RejectedRequests = iRequest.AdminRequests("rejected", userId, role),
                ApprovedRequests = iRequest.AdminRequests("approved", userId, role),
                InProgressRequests = iRequest.AdminRequests("pending", userId, role)
        };
            return PartialView("_RequestChart", result);
        }

        public PartialViewResult GetRequestCalendar(string userId)
        {
            var result = userId;
            return PartialView("_RequestCalendar", result);
        }

        public PartialViewResult GetPartial(string userId, string date)
        {
            var dateTime = DateTime.Parse(date);
            var result = iRequest.GetRequestByData(userId, dateTime);
            return PartialView("_RequestDateInfo", result);
        }
            
            

    }
}
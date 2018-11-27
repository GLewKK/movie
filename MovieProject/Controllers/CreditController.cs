using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Models;
using DAL.UnitOfWork;
using System.Web.Mvc;
using BLL;
using Microsoft.AspNet.Identity;
using MovieProject.ViewModels;

namespace MovieProject.Controllers
{

    public class CreditController : Controller
    {
        // GET: Credit
        private readonly ITakeRequest iRequest = new TakeRequest();

        public ActionResult Credit()
        {

            //Conditie if exist data extra atribuie automat
            var id = HttpContext.User.Identity.GetUserId();
            bool hasCredit = iRequest.CheckCredit(id);
            if(!hasCredit)
            {
                CreditViewModel model = new CreditViewModel();
                //return View(model);
                return View("Credit2", model);
            }

               return RedirectToAction("Request", "Home");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Credit(CreditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var id = HttpContext.User.Identity.GetUserId();
                iRequest.SetCredit(id,model.Sum,model.Months);

                var dateTime = DateTime.Parse(model.DateOfBirthString);
                var data = new DataExtra
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Telephone = model.Telephone,
                    IDNP = model.IDNP,
                    IsMarried = model.IsMarried,
                    OfficialVenit = model.OfficialVenit,
                    OfficialVenitHusbandWife = model.OfficialVenitHusbandWife,
                    TelephoneWorkSpace = model.TelephoneWorkSpace,
                    WorkSpace = model.WorkSpace,
                    WorkSpaceHusbandWife = model.WorkSpaceHusbandWife,
                    DateOfBirth = dateTime
                    
                };
               
               
                iRequest.AddExtraData(data,id);
                return RedirectToAction("Request", "Home");

            }
            if(!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
                return View("Credit2",model);
        }
    }
}
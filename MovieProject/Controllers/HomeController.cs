using System.Web.Mvc;
using MovieProject.ViewModels;

namespace MovieProject.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult Template()
		{
			var model = new TemplateViewModel();
			return View(model);
		}
	}
}
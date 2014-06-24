// ShortStuffApi
// HomeController.cs

using System.Web.Mvc;

namespace ShortStuffApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}

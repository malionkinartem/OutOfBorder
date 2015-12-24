using Microsoft.AspNet.Mvc;

namespace OOB.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

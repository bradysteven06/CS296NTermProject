using Microsoft.AspNetCore.Mvc;


namespace Drone_Enthusiast_Community.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RulesRegulations()
        {
            return View();
        }

        
    }
}

using Microsoft.AspNetCore.Mvc;


namespace Drone_Enthusiast_Community.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Pictures()
        {
            return View();
        }

        public IActionResult Video()
        {
            return View();
        }

        public IActionResult NewPicture()
        {
            return View();
        }

        public IActionResult NewVideo()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}

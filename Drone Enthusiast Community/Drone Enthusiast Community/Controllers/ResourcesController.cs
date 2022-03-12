using Drone_Enthusiast_Community.Data;
using Drone_Enthusiast_Community.Models;
using Drone_Enthusiast_Community.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Controllers
{
    public class ResourcesController : Controller
    {
        IResourceRepository repo;

        public ResourcesController(IResourceRepository r)
        {
            repo = r;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ResourceModel> resourceList = null;
            await Task.Run(() => resourceList = repo.Resources.ToList());

            // Does not work with Unit Test
            //ViewBag.Message = TempData["Message"];
            return View(resourceList);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var file = await repo.Resources.Where(x => x.ResourceID == id).FirstOrDefaultAsync();
            return View(file);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddResource(string description, string name, string address)
        {
            var fileModel = new ResourceModel
            {
                WebsiteName = name,
                Description = description,
                WebAddress = address,
            };
            await repo.AddResourceAsync(fileModel);

            //TempData["Message"] = "File successfully uploaded to File System.";

            return RedirectToAction("Index");
        }

        // deletes resource
        [Authorize]
        public async Task<IActionResult> DeleteResource(int id)
        {
            var file = repo.Resources.Where(x => x.ResourceID == id).FirstOrDefault();

            if (file == null)
            {
                return null;
            }

            await repo.DeleteResourceAsync(file);

            // Does not work with Unit Test
            //TempData["Message"] = $"Removed {file.WebsiteName} successfully from File System.";
            return RedirectToAction("Index");
        }
    }
}

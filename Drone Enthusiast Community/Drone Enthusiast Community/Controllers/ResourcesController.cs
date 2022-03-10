using Drone_Enthusiast_Community.Data;
using Drone_Enthusiast_Community.Models;
using Drone_Enthusiast_Community.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var resourceList = await LoadAllFiles();
            ViewBag.Message = TempData["Message"];
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

            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Index");
        }

        // deletes resource
        [Authorize]
        public async Task<IActionResult> DeleteResource(int id)
        {
            var file = await repo.Resources.Where(x => x.ResourceID == id).FirstOrDefaultAsync();

            if (file == null)
            {
                return null;
            }

            await repo.DeleteResourceAsync(file);

            TempData["Message"] = $"Removed {file.WebsiteName} successfully from File System.";
            return RedirectToAction("Index");
        }

        // gets list of resources from database
        private async Task<FileUploadVM> LoadAllFiles()
        {
            var viewModel = new FileUploadVM();
            viewModel.ResourceList = await repo.Resources.ToListAsync();
            return viewModel;
        }
    }
}

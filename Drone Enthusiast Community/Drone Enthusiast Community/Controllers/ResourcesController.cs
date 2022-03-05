using Drone_Enthusiast_Community.Data;
using Drone_Enthusiast_Community.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly DroneCommDbContext context;

        public ResourcesController(DroneCommDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resourceList = await LoadAllFiles();
            ViewBag.Message = TempData["Message"];
            return View(resourceList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddResource(string description, string name, string address)
        {
            var fileModel = new ResourceModel
            {
                WebsiteName = name,
                Description = description,
                WebAddress = address,
            };
            await context.Resources.AddAsync(fileModel);
            context.SaveChanges();
                
            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Index");
        }

        // deletes resource
        public async Task<IActionResult> DeleteResource(int id)
        {
            var file = await context.Resources.Where(x => x.ResourceID == id).FirstOrDefaultAsync();

            if (file == null)
            {
                return null;
            }

            context.Resources.Remove(file);
            context.SaveChanges();
            TempData["Message"] = $"Removed {file.WebsiteName} successfully from File System.";
            return RedirectToAction("Index");
        }

        // gets list of resources from database
        private async Task<FileUploadVM> LoadAllFiles()
        {
            var viewModel = new FileUploadVM();
            viewModel.ResourceList = await context.Resources.ToListAsync();
            return viewModel;
        }
    }
}

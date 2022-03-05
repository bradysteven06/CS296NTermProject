using Drone_Enthusiast_Community.Data;
using Drone_Enthusiast_Community.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Controllers
{
    public class DronesController : Controller
    {
        private readonly DroneCommDbContext context;

        public DronesController(DroneCommDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var droneList = await LoadAllFiles();
            ViewBag.Message = TempData["Message"];
            return View(droneList);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDrone(List<IFormFile> files, string description, string name, string size, string weight)
        {
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot/DroneUploads\\");
                bool basePathExists = System.IO.Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var filePath = Path.Combine(basePath, file.FileName);
                var extension = Path.GetExtension(file.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var fileModel = new DroneModel
                    {
                        ImageName = fileName,
                        ImageExtension = extension,
                        FilePath = filePath,
                        Name = name,
                        Description = description,
                        Size = size,
                        Weight = weight
                    };
                    await context.Drones.AddAsync(fileModel);
                    context.SaveChanges();
                }
            }
            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Index");
        }

        // Loads list of drones from database
        private async Task<FileUploadVM> LoadAllFiles()
        {
            var viewModel = new FileUploadVM();
            viewModel.DroneFiles = await context.Drones.ToListAsync();
            return viewModel;
        }

        public async Task<IActionResult> DeleteDrone(int id)
        {

            var file = await context.Drones.Where(x => x.DroneID == id).FirstOrDefaultAsync();
            if (file == null)
            {
                return null;
            }

            if (System.IO.File.Exists(file.FilePath))
            {
                System.IO.File.Delete(file.FilePath);
            }
            context.Drones.Remove(file);
            context.SaveChanges();
            TempData["Message"] = $"Removed {file.Name} successfully from File System.";
            return RedirectToAction("Index");
        }
    }
}

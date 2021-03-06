using Drone_Enthusiast_Community.Data;
using Drone_Enthusiast_Community.Models;
using Drone_Enthusiast_Community.Repos;
using Microsoft.AspNetCore.Authorization;
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
        IDroneRepository repo;

        public DronesController(IDroneRepository r)
        {
            repo = r;
        }

        public async Task<IActionResult> Index()
        {
            var droneList = await LoadAllFiles();
            ViewBag.Message = TempData["Message"];
            return View(droneList);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var file = await repo.Drones.Where(x => x.DroneID == id).FirstOrDefaultAsync();
            return View(file);
        }

        /*
         * TODO - Add file type validation
         * TODO - Refactor code for single file instead of list
         * TODO - set maximum image upload size
         */
        [HttpPost]
        [Authorize]
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
                    await repo.AddDroneAsync(fileModel);
                    TempData["Message"] = "File successfully uploaded.";
                }
                else
                {
                    TempData["Message"] = "Upload failed. Filename already taken.";
                }
            }
            
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> DeleteDrone(int id)
        {

            var file = await repo.Drones.Where(x => x.DroneID == id).FirstOrDefaultAsync();
            if (file == null)
            {
                return null;
            }

            if (System.IO.File.Exists(file.FilePath))
            {
                System.IO.File.Delete(file.FilePath);
            }
            await repo.DeleteDroneAsync(file);
            TempData["Message"] = $"Removed {file.Name} successfully from File System.";
            return RedirectToAction("Index");
        }

        // Gets list of drones
        private async Task<FileUploadVM> LoadAllFiles()
        {
            var viewModel = new FileUploadVM();
            viewModel.DroneFiles = await repo.Drones.ToListAsync();
            return viewModel;
        }
    }
}

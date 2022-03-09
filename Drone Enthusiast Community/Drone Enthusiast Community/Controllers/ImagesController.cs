using Drone_Enthusiast_Community.Data;
using Drone_Enthusiast_Community.Models;
using Drone_Enthusiast_Community.Repos;
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
    public class ImagesController : Controller
    {
        IImageRepository repo;

        public ImagesController(IImageRepository r)
        {
            repo = r;
        }
        public async Task<IActionResult> Index()
        {
            var imageList = await LoadAllFiles();
            ViewBag.Message = TempData["Message"];
            return View(imageList);
        }

        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var file = await repo.GetImageByIDAsync(id);
            return View(file);
        }

        /*
         * TODO - Add file type validation
         * TODO - Refactor code for single file instead of list
         * TODO - set maximum image upload size
         */

        // uploads an image
        [HttpPost]
        public async Task<IActionResult> UploadImage(List<IFormFile> files, string description)
        {
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot/ImageUploads\\");
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
                    var fileModel = new ImageModel
                    {
                        Date = DateTime.UtcNow,
                        Title = fileName,
                        FilePath = filePath,
                        Extension = extension,
                        Description = description
                    };
                    await repo.AddImageAsync(fileModel);
                    TempData["Message"] = "File successfully uploaded.";
                }
                else
                {
                    TempData["Message"] = "Upload failed. Filename already taken.";
                }
            }
            return RedirectToAction("Index");
        }

        // gets single image object for view
        public async Task<IActionResult> ViewImage(int id)
        {
            var file = await repo.GetImageByIDAsync(id);
            return View(file);
        }

        // deletes image
        public async Task<IActionResult> DeleteImage(int id)
        {

            var file = await repo.GetImageByIDAsync(id);
            if (file == null) 
            { 
                return null; 
            }
                
            if (System.IO.File.Exists(file.FilePath))
            {
                System.IO.File.Delete(file.FilePath);
            }
            await repo.DeleteImageAsync(file);
            TempData["Message"] = $"Removed {file.Title} successfully from File System.";
            return RedirectToAction("Index");
        }

        // Gets list of images
        private async Task<FileUploadVM> LoadAllFiles()
        {
            var viewModel = new FileUploadVM();
            viewModel.ImageFiles = await repo.Images.ToListAsync();
            return viewModel;
        }
    }
}

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
    public class ImagesController : Controller
    {
        private readonly DroneCommDbContext context;

        public ImagesController(DroneCommDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var imageList = await LoadAllFiles();
            ViewBag.Message = TempData["Message"];
            return View(imageList);
        }
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
                        Extension = extension
                    };
                    context.Images.Add(fileModel);
                    context.SaveChanges();
                }
            }
            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Index");
        }
       

        private async Task<FileUploadVM> LoadAllFiles()
        {
            var viewModel = new FileUploadVM();
            viewModel.ImageFiles = await context.Images.ToListAsync();
            return viewModel;
        }
                
        /*public async Task<IActionResult> DownloadFileFromFileSystem(int id)
        {

            var file = await context.FilesOnFileSystem.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (file == null) return null;
            var memory = new MemoryStream();
            using (var stream = new FileStream(file.FilePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, file.FileType, file.Name + file.Extension);
        }*/

        public async Task<IActionResult> DeleteImage(int id)
        {

            var file = await context.Images.Where(x => x.ImageID == id).FirstOrDefaultAsync();
            if (file == null) 
            { 
                return null; 
            }
                
            if (System.IO.File.Exists(file.FilePath))
            {
                System.IO.File.Delete(file.FilePath);
            }
            context.Images.Remove(file);
            context.SaveChanges();
            TempData["Message"] = $"Removed {file.Title} successfully from File System.";
            return RedirectToAction("Index");
        }
    }
}

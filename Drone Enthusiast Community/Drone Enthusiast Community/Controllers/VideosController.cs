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
    public class VideosController : Controller
    {
        private readonly DroneCommDbContext context;

        public VideosController(DroneCommDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var videoList = await LoadAllFiles();
            ViewBag.Message = TempData["Message"];
            return View(videoList);
        }

        /*
         * TODO - set maximum video upload size
         */
        [HttpPost]
        public async Task<IActionResult> UploadVideo(List<IFormFile> files, string description)
        {
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot/VideoUploads\\");
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
                    var fileModel = new VideoModel
                    {
                        Date = DateTime.UtcNow,
                        Title = fileName,
                        FilePath = filePath,
                        Extension = extension,
                        Description = description
                    };
                    context.Videos.Add(fileModel);
                    context.SaveChanges();
                }
            }
            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ViewVideo(int id)
        {
            var file = await context.Videos.Where(x => x.VideoID == id).FirstOrDefaultAsync();
            return View(file);
        }

        private async Task<FileUploadVM> LoadAllFiles()
        {
            var viewModel = new FileUploadVM();
            viewModel.VideoFiles = await context.Videos.ToListAsync();
            return viewModel;
        }

        public async Task<IActionResult> DeleteVideo(int id)
        {

            var file = await context.Videos.Where(x => x.VideoID == id).FirstOrDefaultAsync();
            if (file == null)
            {
                return null;
            }

            if (System.IO.File.Exists(file.FilePath))
            {
                System.IO.File.Delete(file.FilePath);
            }
            context.Videos.Remove(file);
            context.SaveChanges();
            TempData["Message"] = $"Removed {file.Title} successfully from File System.";
            return RedirectToAction("Index");
        }
    }
}

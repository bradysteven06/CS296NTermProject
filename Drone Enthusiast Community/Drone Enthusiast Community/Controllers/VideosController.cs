using Drone_Enthusiast_Community.Data;
using Drone_Enthusiast_Community.Models;
using Drone_Enthusiast_Community.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        IVideoRepository repo;
        UserManager<AppUser> userManager;

        public VideosController(IVideoRepository r, UserManager<AppUser> u)
        {
            repo = r;
            userManager = u;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<VideoModel> videoList = null;
            await Task.Run(() => videoList = repo.Videos.ToList());
            ViewBag.Message = TempData["Message"];
            return View(videoList);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {

            var file = await repo.Videos.Where(x => x.VideoID == id).FirstOrDefaultAsync();
            return View(file);
        }

        /*
         * TODO - Fix file type validation message. Does not display
         * TODO - Refactor code for single file instead of list
         * TODO - Allow larger files, look into chunking file
         * TODO - Display Uploaded by
         * TODO - Display error for files larger than limit
         */
        [HttpPost]
        [Authorize]
        [RequestSizeLimit(25_000_000)]
        public async Task<IActionResult> UploadVideo(List<IFormFile> files, string description)
        {
            var allowedExtensions = new[] { ".mp4", ".mov"};

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
                    if (allowedExtensions.Contains(extension.ToLower()))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        var fileModel = new VideoModel
                        {
                            Date = DateTime.Now,
                            Title = fileName,
                            FilePath = filePath,
                            Extension = extension,
                            Description = description,
                            Uploader = await userManager.GetUserAsync(User)
                        };
                        await repo.AddVideoAsync(fileModel);
                        TempData["Message"] = "File successfully uploaded.";
                    }
                    else
                    {
                        TempData["Mesage"] = "Upload failed. Unsupported file type. Use mp4, mov.";
                    }
                }
                else
                {
                    TempData["Message"] = "Upload failed. Filename already taken.";
                }
                
                

            }
            
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> ViewVideo(int id)
        {
            var file = await repo.Videos.Where(x => x.VideoID == id).FirstOrDefaultAsync();
            return View(file);
        }

        [Authorize]
        public async Task<IActionResult> DeleteVideo(int id)
        {

            var file = await repo.Videos.Where(x => x.VideoID == id).FirstOrDefaultAsync();
            if (file == null)
            {
                return null;
            }

            if (System.IO.File.Exists(file.FilePath))
            {
                System.IO.File.Delete(file.FilePath);
            }
            await repo.DeleteVideoAsync(file);
            TempData["Message"] = $"Removed {file.Title} successfully from File System.";
            return RedirectToAction("Index");
        }
    }
}

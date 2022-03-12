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
    public class ImagesController : Controller
    {
        IImageRepository repo;
        UserManager<AppUser> userManager;

        public ImagesController(IImageRepository r, UserManager<AppUser> u)
        {
            repo = r;
            userManager = u;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<ImageModel> imageList = null;
            await Task.Run(() => imageList = repo.Images.ToList());
            ViewBag.Message = TempData["Message"];
            return View(imageList);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var file = await repo.Images.Where(x => x.ImageID == id).FirstOrDefaultAsync(); ;
            return View(file);
        }

        /*
         * TODO - Fix file type validation message. Does not display
         * TODO - Refactor code for single file instead of list
         * TODO - Display Uploaded by
         * TODO - Display error for files larger than limit
         */

        // uploads an image
        [HttpPost]
        [Authorize]
        [RequestSizeLimit(25_000_000)]
        public async Task<IActionResult> UploadImage(List<IFormFile> files, string description)
        {
            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif" };

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
                    if (allowedExtensions.Contains(extension.ToLower()))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        var fileModel = new ImageModel
                        {
                            Date = DateTime.Now,
                            Title = fileName,
                            FilePath = filePath,
                            Extension = extension,
                            Description = description,
                            Uploader = await userManager.GetUserAsync(User)
                        };
                        await repo.AddImageAsync(fileModel);
                        TempData["Message"] = "File successfully uploaded.";
                    }
                    else
                    {
                        TempData["Mesage"] = "Upload failed. Unsupported file type. Use jpg, jpeg, png, gif.";
                    }
                }
                else
                {
                    TempData["Message"] = "Upload failed. Filename already taken.";
                }
            }
            return RedirectToAction("Index");
        }

        // gets single image object for view
        [Authorize]
        public async Task<IActionResult> ViewImage(int id)
        {
            var file = await repo.Images.Where(x => x.ImageID == id).FirstOrDefaultAsync(); ;
            return View(file);
        }

        // deletes image
        [Authorize]
        public async Task<IActionResult> DeleteImage(int id)
        {

            var file = await repo.Images.Where(x => x.ImageID == id).FirstOrDefaultAsync(); ;
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
    }
}

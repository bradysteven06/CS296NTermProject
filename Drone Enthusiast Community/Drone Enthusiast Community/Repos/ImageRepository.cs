using Drone_Enthusiast_Community.Data;
using Drone_Enthusiast_Community.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Repos
{
    public class ImageRepository : IImageRepository
    {
        private DroneCommDbContext context;

        public ImageRepository(DroneCommDbContext c)
        {
            context = c;
        }

        // Returns image objects
        public IQueryable<ImageModel> Images
        {
            get
            {
                // Get all the image objects
                return context.Images;
            }
        }

        // Returns a image object
        public async Task<ImageModel> GetImageByIDAsync(int id)
        {
            return await context.Images.Where(x => x.ImageID == id).FirstOrDefaultAsync();
        }

        // Add a image
        public async Task<int> AddImageAsync(ImageModel image)
        {
            await context.Images.AddAsync(image);
            return await context.SaveChangesAsync();
        }

        // Delete a image
        public async Task<int> DeleteImageAsync(ImageModel image)
        {
            context.Images.Remove(image);
            return await context.SaveChangesAsync();
        }
    }
}

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

        // Add a image
        public async Task AddImageAsync(ImageModel image)
        {
            await context.Images.AddAsync(image);
            await context.SaveChangesAsync();
        }

        // Delete a image
        public async Task DeleteImageAsync(ImageModel image)
        {
            context.Images.Remove(image);
            await context.SaveChangesAsync();
        }
    }
}

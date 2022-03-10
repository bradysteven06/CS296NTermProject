using Drone_Enthusiast_Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Repos
{
    public class FakeImageRepository : IImageRepository
    {
        List<ImageModel> images = new List<ImageModel>();

        // Returns image objects
        public IQueryable<ImageModel> Images
        {
            get
            {
                // Get all the image objects
                return images.AsQueryable();
            }
        }

        // Add a image
        public async Task AddImageAsync(ImageModel image)
        {
            image.ImageID = images.Count;
            await Task.Run(() => images.Add(image));
        }

        // Delete a image
        public async Task DeleteImageAsync(ImageModel image)
        {
            await Task.Run(() => images.RemoveAt(image.ImageID));
        }
    }
}

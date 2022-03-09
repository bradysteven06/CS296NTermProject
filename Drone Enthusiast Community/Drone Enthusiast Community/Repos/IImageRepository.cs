using Drone_Enthusiast_Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Repos
{
    public interface IImageRepository
    {
        IQueryable<ImageModel> Images { get; } // Returns image objects
        Task<ImageModel> GetImageByIDAsync(int id); // Returns a image object
        Task<int> AddImageAsync(ImageModel resource); // Add a image
        Task<int> DeleteImageAsync(ImageModel resource); // Delete a image
    }
}

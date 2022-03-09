using Drone_Enthusiast_Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Repos
{
    public interface IVideoRepository
    {
        IQueryable<VideoModel> Videos { get; } // Returns video objects
        Task<VideoModel> GetVideoByIDAsync(int it); // Returns a video object
        Task<int> AddVideoAsync(VideoModel video); // Add a video
        Task<int> DeleteVideoAsync(VideoModel video); // Delete a video
    }
}

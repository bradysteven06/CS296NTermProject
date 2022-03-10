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
        Task AddVideoAsync(VideoModel video); // Add a video
        Task DeleteVideoAsync(VideoModel video); // Delete a video
    }
}

using Drone_Enthusiast_Community.Data;
using Drone_Enthusiast_Community.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Repos
{
    public class VideoRepository : IVideoRepository
    {
        private DroneCommDbContext context;

        public VideoRepository(DroneCommDbContext c)
        {
            context = c;
        }

        // Returns video objects
        public IQueryable<VideoModel> Videos
        {
            get
            {
                return context.Videos;
            }
        }

        // Add a video
        public async Task AddVideoAsync(VideoModel video)
        {
            await context.Videos.AddAsync(video);
            await context.SaveChangesAsync();
        }

        // Delete a video
        public async Task DeleteVideoAsync(VideoModel video)
        {
            context.Videos.Remove(video);
            await context.SaveChangesAsync();
        }
    }
}

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

        // Returns a video object
        public async Task<VideoModel> GetVideoByIDAsync(int id)
        {
            return await context.Videos.Where(x => x.VideoID == id).FirstOrDefaultAsync();
        }

        // Add a video
        public async Task<int> AddVideoAsync(VideoModel video)
        {
            await context.Videos.AddAsync(video);
            return await context.SaveChangesAsync();
        }

        // Delete a video
        public async Task<int> DeleteVideoAsync(VideoModel video)
        {
            context.Videos.Remove(video);
            return await context.SaveChangesAsync();
        }
    }
}

using Drone_Enthusiast_Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Repos
{
    public class FakeVideoRepository
    {
        List<VideoModel> videos = new List<VideoModel>();

        // Returns video objects
        public IQueryable<VideoModel> Videos
        {
            get
            {
                return videos.AsQueryable<VideoModel>();
            }
        }

        // Add a video
        public async Task AddVideoAsync(VideoModel video)
        {
            video.VideoID = videos.Count;
            await Task.Run(() => videos.Add(video));
        }

        // Delete a video
        public async Task DeleteVideoAsync(VideoModel video)
        {
            await Task.Run(() => videos.RemoveAt(video.VideoID));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Models
{
    public class FileUploadVM
    {
        public List<ImageModel> ImageFiles { get; set; }
        public List<VideoModel> VideoFiles { get; set; }
        public List<DroneModel> DroneFiles { get; set; }
        public List<ResourceModel> ResourceList { get; set; }
    }
}

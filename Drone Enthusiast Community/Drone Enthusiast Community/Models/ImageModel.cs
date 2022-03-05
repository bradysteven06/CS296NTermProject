using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Models
{
    public class ImageModel
    {
        [Key]
        public int ImageID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string Extension { get; set; }
        public DateTime Date { get; set; }
        //public AppUser Uploader;
    }
}

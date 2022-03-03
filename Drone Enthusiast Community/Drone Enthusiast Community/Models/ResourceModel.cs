using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Models
{
    public class ResourceModel
    {
        [Key]
        public int ResourceID { get; set; }
        [Required]
        public string WebsiteName { get; set; }
        public string Description { get; set; }
    }
}

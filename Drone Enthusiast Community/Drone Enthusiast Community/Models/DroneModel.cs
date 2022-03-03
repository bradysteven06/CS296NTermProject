using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Models
{
    public class DroneModel
    {
        [Key]
        public int DroneID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
    }
}

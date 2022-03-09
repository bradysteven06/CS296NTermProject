using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;


namespace Drone_Enthusiast_Community.Models
{
    public class AdminVM
    {
        public IEnumerable<AppUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}

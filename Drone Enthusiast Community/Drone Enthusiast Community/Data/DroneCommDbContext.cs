using Drone_Enthusiast_Community.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Data
{
    public class DroneCommDbContext : DbContext
    {
        public DroneCommDbContext(DbContextOptions<DroneCommDbContext> options) : base(options) { }

        public DbSet<DroneModel> Drones { get; set; }
        public DbSet<ResourceModel> Resources { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<VideoModel> Videos { get; set; }
    }
}

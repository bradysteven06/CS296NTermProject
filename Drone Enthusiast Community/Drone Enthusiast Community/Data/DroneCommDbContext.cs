using Drone_Enthusiast_Community.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Drone_Enthusiast_Community.Data
{
    public class DroneCommDbContext : IdentityDbContext<AppUser>
    {
        public DroneCommDbContext(DbContextOptions<DroneCommDbContext> options) : base(options) { }

        public DbSet<DroneModel> Drones { get; set; }
        public DbSet<ResourceModel> Resources { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<VideoModel> Videos { get; set; }

        // adds initial values to database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // set primary keys
            modelBuilder.Entity<ResourceModel>().HasKey(resource => new { resource.ResourceID });

            // seed initial data
            //modelBuilder.Seed();
        }
    }
}

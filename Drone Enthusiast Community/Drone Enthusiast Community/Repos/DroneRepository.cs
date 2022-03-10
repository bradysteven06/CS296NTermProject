using Drone_Enthusiast_Community.Data;
using Drone_Enthusiast_Community.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Repos
{
    public class DroneRepository : IDroneRepository
    {
        private DroneCommDbContext context;

        public DroneRepository(DroneCommDbContext c)
        {
            context = c;
        }

        // Returns drone objects
        public IQueryable<DroneModel> Drones
        {
            get
            {
                // Get all the drone objects
                return context.Drones;
            }
        }

        // Add a drone
        public async Task AddDroneAsync(DroneModel drone)
        {
            await context.Drones.AddAsync(drone);
            await context.SaveChangesAsync();
        }

        // Delete a drone
        public async Task DeleteDroneAsync(DroneModel drone)
        {
            context.Drones.Remove(drone);
            await context.SaveChangesAsync();
        }
    }
}

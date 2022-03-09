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

        // Returns a drone object
        public async Task<DroneModel> GetDroneByIDAsync(int id)
        {
            return await context.Drones.Where(x => x.DroneID == id).FirstOrDefaultAsync();
        }

        // Add a drone
        public async Task<int> AddDroneAsync(DroneModel drone)
        {
            await context.Drones.AddAsync(drone);
            return await context.SaveChangesAsync();
        }

        // Delete a drone
        public async Task<int> DeleteDroneAsync(DroneModel drone)
        {
            context.Drones.Remove(drone);
            return await context.SaveChangesAsync();
        }
    }
}

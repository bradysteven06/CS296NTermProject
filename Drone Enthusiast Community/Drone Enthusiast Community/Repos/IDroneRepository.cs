using Drone_Enthusiast_Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Repos
{
    public interface IDroneRepository
    {
        IQueryable<DroneModel> Drones { get; } // Returns drone objects
        Task AddDroneAsync(DroneModel drone); // Add a drone
        Task DeleteDroneAsync(DroneModel drone); // Delete a drone
    }
}

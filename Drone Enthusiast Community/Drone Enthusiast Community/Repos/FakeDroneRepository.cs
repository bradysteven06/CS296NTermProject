using Drone_Enthusiast_Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Repos
{
    public class FakeDroneRepository : IDroneRepository
    {
        List<DroneModel> drones = new List<DroneModel>();

        public IQueryable<DroneModel> Drones
        {
            get { return drones.AsQueryable<DroneModel>(); }
        }

        public async Task AddDroneAsync(DroneModel drone)
        {
            drone.DroneID = drones.Count;
            await Task.Run(() => drones.Add(drone));
        }

        public async Task DeleteDroneAsync(DroneModel drone)
        {
            await Task.Run(() => drones.RemoveAt(drone.DroneID));
        }
    }
}

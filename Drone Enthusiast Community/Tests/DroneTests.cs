using Drone_Enthusiast_Community.Models;
using Drone_Enthusiast_Community.Controllers;
using Drone_Enthusiast_Community.Repos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Threading.Tasks;
using System;

namespace Tests
{
    public class DroneTests
    {
        [Fact]
        public async Task AddDroneAsyncTest()
        {
            // Arrange
            var fakeRepo = new FakeDroneRepository();
            var controller = new DronesController(fakeRepo);

            var drone1 = new DroneModel() { Name = "Drone 1"};
            await fakeRepo.AddDroneAsync(drone1);
            var drone2 = new DroneModel() { Name = "Drone 2" };
            await fakeRepo.AddDroneAsync(drone2);

            // Act
            var viewResult = (ViewResult)controller.Index().Result;

            // Assert
            
            var drones = (List<DroneModel>)viewResult.ViewData.Model;
            Assert.Equal(2, drones.Count);
            Assert.Equal(drones[0].Name, drone1.Name);
            Assert.Equal(drones[1].Name, drone2.Name);
        }
    }
}

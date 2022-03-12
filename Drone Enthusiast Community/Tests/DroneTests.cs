using Drone_Enthusiast_Community.Models;
using Drone_Enthusiast_Community.Controllers;
using Drone_Enthusiast_Community.Repos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Tests
{
    public class DroneTests
    {
        // Did not have time to figure out how to test with IFormFile
        /*[Fact]
        public async Task AddDroneAsyncTest()
        {
            using (var stream = File.OpenRead(@"wwwroot /Images"))
            {
                var file = new FormFile(stream, 0, stream.Length, null,
                    Path.GetFileName(@"wwwroot/Images/mavic_air_2.jpg"))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };

                var controller = new DronesController();
                var result = controller.AddDrone(file, "t", "t", "t", "t");
                Assert.IsAssignableFrom<OkResult>(result);
            }
            
        }*/

        [Fact]
        public async Task DeleteDroneAsyncTest()
        {

        }
    }
}

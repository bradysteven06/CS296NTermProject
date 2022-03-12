using Drone_Enthusiast_Community.Models;
using Drone_Enthusiast_Community.Controllers;
using Drone_Enthusiast_Community.Repos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Tests
{
    public class ResourceTests
    {
        [Fact]
        public async Task AddResourceAsyncTest()
        {
            // Arrange
            var fakeRepo = new FakeResourceRepository();
            var controller = new ResourcesController(fakeRepo);

            var resource1 = new ResourceModel() { Description = "A", WebAddress = "B", WebsiteName = "C" };
            var resource2 = new ResourceModel() { Description = "D", WebAddress = "E", WebsiteName = "F" };

            // Act
            await controller.AddResource(resource1.Description, resource1.WebsiteName, resource1.WebAddress);
            await controller.AddResource(resource2.Description, resource2.WebsiteName, resource2.WebAddress);
            var viewResult = (ViewResult)controller.Index().Result;

            // Assert
            var resources = (List<ResourceModel>)viewResult.ViewData.Model;
            Assert.Equal(resources[0].Description, resource1.Description);
            Assert.Equal(resources[1].Description, resource2.Description);
        }


        // Not working yet
        [Fact]
        public async Task DeleteResourceAsyncTest()
        {
            // Arrange
            var fakeRepo = new FakeResourceRepository();
            var controller = new ResourcesController(fakeRepo);

            var resource1 = new ResourceModel() { Description = "A", WebAddress = "B", WebsiteName = "C" };
            await controller.AddResource(resource1.Description, resource1.WebsiteName, resource1.WebAddress);

            // Act
            var viewResult = (ViewResult)controller.Index().Result;
            var resources = (List<ResourceModel>)viewResult.ViewData.Model;
            //await controller.DeleteResource(resources[0].ResourceID); // does not remove?

            // Assert

            Assert.Empty(resources);
        }
    }
}

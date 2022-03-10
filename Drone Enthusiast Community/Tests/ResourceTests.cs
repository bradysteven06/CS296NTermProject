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
    public class ResourceTests
    {
        [Fact]
        public async Task AddResourceAsyncTest()
        {
            var fakeRepo = new FakeResourceRepository();
            var controller = new ResourcesController(fakeRepo);

            var resource1 = new ResourceModel() { ResourceID = 1, Description = "A", WebAddress = "B", WebsiteName = "C" };
            fakeRepo.AddResourceAsync(resource1);

            var viewResult = (ViewResult)controller.Index().Result;

            var resources = (List<ResourceModel>)viewResult.ViewData.Model;
            Assert.Equal(1, resources.Count);
            Assert.Equal(resources[0].ResourceID, resource1.ResourceID);
        }
    }
}

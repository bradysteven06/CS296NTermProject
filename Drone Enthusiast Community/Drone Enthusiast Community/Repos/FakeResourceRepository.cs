using Drone_Enthusiast_Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Repos
{
    public class FakeResourceRepository : IResourceRepository
    {
        List<ResourceModel> resources = new List<ResourceModel>();

        // Returns resource objects
        public IQueryable<ResourceModel> Resources
        {
            get
            {
                // Get all the Resource objects
                return resources.AsQueryable<ResourceModel>();
            }
        }

        // Add a resource
        public async Task AddResourceAsync(ResourceModel resource)
        {
            resource.ResourceID = resources.Count;
            await Task.Run(() => resources.Add(resource));
        }

        // Delete a resource
        public async Task DeleteResourceAsync(ResourceModel resource)
        {
            resources.RemoveAt(resource.ResourceID);
            //await Task.Run(() => resources.RemoveAt(resource.ResourceID));
        }
    }
}

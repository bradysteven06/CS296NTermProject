using Drone_Enthusiast_Community.Data;
using Drone_Enthusiast_Community.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Repos
{
    public class ResourceRepository : IResourceRepository
    {
        private DroneCommDbContext context;

        public ResourceRepository(DroneCommDbContext c)
        {
            context = c;
        }

        // Returns resource objects
        public IQueryable<ResourceModel> Resources 
        { 
            get
            {
                // Get all the Resource objects
                return context.Resources;
            }
        }

        // Add a resource
        public async Task AddResourceAsync(ResourceModel resource)
        {
            await context.Resources.AddAsync(resource);
            await context.SaveChangesAsync();
        }

        // Delete a resource
        public async Task DeleteResourceAsync(ResourceModel resource)
        {
            context.Resources.Remove(resource);
            await context.SaveChangesAsync();
        }
    }
}

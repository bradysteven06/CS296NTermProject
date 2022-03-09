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

        // Returns a resource object
        public async Task<ResourceModel> GetResourceByIDAsync(int id)
        {
            return await context.Resources.Where(x => x.ResourceID == id).FirstOrDefaultAsync();
        }

        // Add a resource
        public async Task<int> AddResourceAsync(ResourceModel resource)
        {
            await context.Resources.AddAsync(resource);
            return await context.SaveChangesAsync();
        }

        // Delete a resource
        public async Task<int> DeleteResourceAsync(ResourceModel resource)
        {
            context.Resources.Remove(resource);
            return await context.SaveChangesAsync();
        }
    }
}

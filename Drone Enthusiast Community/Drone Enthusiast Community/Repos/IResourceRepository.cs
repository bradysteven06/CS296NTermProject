using Drone_Enthusiast_Community.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Repos
{
    public interface IResourceRepository
    {
        IQueryable<ResourceModel> Resources { get; } // Returns resource objects
        Task<ResourceModel> GetResourceByIDAsync(int id); // Returns a resource object
        Task<int> AddResourceAsync(ResourceModel resource); // Add a resource
        Task<int> DeleteResourceAsync(ResourceModel resource); // Delete a resource
    }
}

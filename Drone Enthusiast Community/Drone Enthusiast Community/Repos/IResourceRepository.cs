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
        Task AddResourceAsync(ResourceModel resource); // Add a resource
        Task DeleteResourceAsync(ResourceModel resource); // Delete a resource
    }
}

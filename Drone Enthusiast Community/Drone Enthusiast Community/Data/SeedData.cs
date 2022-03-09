using Drone_Enthusiast_Community.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community.Data
{
    public static class SeedData
    {
        public static async Task SeedAdminUser(IServiceProvider serviceProvider)
        {
            // TODO: Remove the user name and password from source code
            const string USER_NAME = "admin";
            const string SCREEN_NAME = "Admin";
            const string PASS_WORD = "Secret!123";
            const string ROLE_NAME = "Admin";

            UserManager<AppUser> userManager =
                serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(ROLE_NAME) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(ROLE_NAME));
            }
            // if username doesn't exist, create it and add it to role if (await userManager.FindByNameAsync(username) == null) { User user = new User { UserName = username }; var result = await userManager.CreateAsync(user, password); if (result.Succeeded) {
            if (await userManager.FindByNameAsync(USER_NAME) == null)
            {
                var user = new AppUser { UserName = USER_NAME, Name = SCREEN_NAME };
                var result = await userManager.CreateAsync(user, PASS_WORD);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, ROLE_NAME);
                }
            }
        }

        public static async Task SeedUser(IServiceProvider serviceProvider)
        {
            // TODO: Remove the user name and password from source code
            const string USER_NAME = "deadP";
            const string SCREEN_NAME = "Dead Pool";
            const string PASS_WORD = "Secret!123";
            const string ROLE_NAME = "User";

            UserManager<AppUser> userManager =
                serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(ROLE_NAME) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(ROLE_NAME));
            }
            // if username doesn't exist, create it and add it to role if (await userManager.FindByNameAsync(username) == null) { User user = new User { UserName = username }; var result = await userManager.CreateAsync(user, password); if (result.Succeeded) {
            if (await userManager.FindByNameAsync(USER_NAME) == null)
            {
                var user = new AppUser { UserName = USER_NAME, Name = SCREEN_NAME };
                var result = await userManager.CreateAsync(user, PASS_WORD);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, ROLE_NAME);
                }
            }
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResourceModel>().HasData(
                new
                {
                    ResourceID = 1,
                    WebsiteName = "Federal Aviation Administration",
                    WebAddress = "www.faa.gov/uas/",
                    Description = "FAA drone page"
                },
                new
                {
                    ResourceID = 2,
                    WebsiteName = "Google",
                    WebAddress = "www.google.com",
                    Description = "FAA drone page"
                }
                );
        }
    }
}

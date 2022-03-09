using Drone_Enthusiast_Community.Data;
using Drone_Enthusiast_Community.Models;
using Drone_Enthusiast_Community.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drone_Enthusiast_Community
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // enable dependency injection of repositories into controllers
            services.AddTransient<IResourceRepository, ResourceRepository>();
            services.AddTransient<IDroneRepository, DroneRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IVideoRepository, VideoRepository>();

            // enable dependency injection for dbcontext
            services.AddDbContext<DroneCommDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // adds identity service
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<DroneCommDbContext>()
                .AddDefaultTokenProviders();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            SeedData.SeedAdminUser(app.ApplicationServices).Wait();
        }
    }
}

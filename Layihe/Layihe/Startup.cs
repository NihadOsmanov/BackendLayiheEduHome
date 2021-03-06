using Layihe.Areas.AdminPanel.Utils;
using Layihe.DataAccesLayer;
using Layihe.Models;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment enviroment)
        {
            _configuration = configuration;
            _enviroment = enviroment;
        }

        private readonly IConfiguration _configuration;
        public readonly IWebHostEnvironment _enviroment;


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession((options) =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(30);

            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
                options.Lockout.AllowedForNewUsers = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                options.UseSqlServer(connectionString, builder =>
                {
                    builder.MigrationsAssembly(nameof(Layihe));
                });
            });
            services.AddControllersWithViews();

            Constants.CourseImageFolderPath = Path.Combine(_enviroment.WebRootPath, "img","course");
            Constants.BlogImageFolderPath = Path.Combine(_enviroment.WebRootPath, "img","blog");
            Constants.TeacherImageFolderPath = Path.Combine(_enviroment.WebRootPath, "img","teacher");
            Constants.EventImageFolderPath = Path.Combine(_enviroment.WebRootPath, "img","event");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

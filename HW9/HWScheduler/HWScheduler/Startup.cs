using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HWScheduler.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HWScheduler
{
    public class Startup
    {
        private string connectionString = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("AHWDb"));
            //builder.Password = Configuration["HWSch:dbpass"];
            services.AddControllersWithViews();
            services.AddDbContext<HWDbContext>(opts =>
            {
                //opts.UseSqlServer(Configuration["ConnectionStrings:HWDb"]);
                opts.UseSqlServer(Configuration.GetConnectionString("AHW"));
            });
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
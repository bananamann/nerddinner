using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Owin.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.Logging;
using NerdDinner.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Diagnostics.Entity;
using Owin;
using Microsoft.AspNet.Diagnostics.Entity.Views;

namespace NerdDinner.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Setup configuration sources.
            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add MVC services to the services container.
            services.AddMvc();

            services.AddEntityFramework()
                    .AddInMemoryDatabase()
                    .AddDbContext<ApplicationDbContext>();

            // var runningOnMono = Type.GetType("Mono.Runtime") != null;

            //    // Add EF services to the services container
            //    if (runningOnMono)
            //    {
            //        services.AddEntityFramework()
            //                .AddSQLite()
            //                .AddDbContext<ApplicationDbContext>();
            //    }
            //    else
            //    {
            //     // Add EF services to the services container.
            //   services.AddEntityFramework(Configuration)
            //       .AddSqlServer()
            //       .AddDbContext<ApplicationDbContext>();
            //    }

            // services.SetupOptions<ApplicationDbContextOptions>(options =>
            //                         {

            //                             if (runningOnMono)
            //                             {
            //                                 options.UseSQLite(configuration.Get("Data:SQLiteConnection:ConnectionString");
            //                             }
            //                             else
            //                             {
            //                                 options.UseSqlServer(configuration.Get("Data:DefaultConnection:ConnectionString"));
            //                             }
            //                         });




            // Add Identity services to the services container.
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            // Uncomment the following line to add Web API servcies which makes it easier to port Web API 2 controllers.
            // You need to add Microsoft.AspNet.Mvc.WebApiCompatShim package to project.json
            // services.AddWebApiConventions();

        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            // Configure the HTTP request pipeline.
            // Add the console logger.
            loggerfactory.AddConsole();

            // Add the following to the request pipeline only in development environment.
            if (string.Equals(env.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase))
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                app.UseExceptionHandler("/Home/Error");
            }






            // Add static files to the request pipeline.
            app.UseStaticFiles();

            // Add cookie-based authentication to the request pipeline.
            app.UseIdentity();

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
        }
    }
}

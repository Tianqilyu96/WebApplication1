using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;



namespace WebApplication1
{
    public class Startup
    {
        public readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<FormatService>();
            services.AddSingleton<FeatureToggles>(x => new FeatureToggles 
            { DeveloperExceptions = configuration.GetValue<bool>("FeatureToggles:DevelopementExceptions")});
            services.AddMvc(x => x.EnableEndpointRouting = false);
            services.AddDbContext<BlogData>(options =>
            {
                var connection = configuration.GetConnectionString("BlogData");
                options.UseSqlServer(connection);
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FeatureToggles feature)
        {
            app.UseExceptionHandler("/error.html");

            if (feature.DeveloperExceptions)
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (content, next) => {
                if (content.Request.Path.Value.Contains("invalid"))
                {
                    throw new Exception("error!");
                }
                await next();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("Default", "{controller=Home}/{action = Index}/{id:int?}");
            });

            
            app.UseFileServer();
        }
    }
}

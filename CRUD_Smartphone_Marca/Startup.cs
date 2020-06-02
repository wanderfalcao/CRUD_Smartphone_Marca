﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CRUD_Smartphone_Marca.InversionOfControl;
using CRUD_Smartphone_Marca.MVC.Extensions;
using CRUD_Smartphone_Marca.Identity.Crosscutting;

namespace CRUD_Smartphone_Marca
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();

            //DependencyInjection.RegisterInjections(services, Configuration);

            services.RegisterInjections(Configuration);
            services.RegisterConfigurations(Configuration);
            services.RegisterIdentity(Configuration);

            services.AddAuthorization(
                options => options.AddPolicy("Admin", policy => policy.RequireClaim("AdminClaim")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
                    pattern: "{controller=Marca}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}

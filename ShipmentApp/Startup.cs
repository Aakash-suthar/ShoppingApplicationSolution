using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ShipmentApp.Data;

namespace ShipmentApp
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
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
         .AddCookie("Cookies")
         .AddOpenIdConnect("oidc", options =>
         {
             options.SignInScheme = "Cookies";
             options.Authority = "https://authenticationapp2.azurewebsites.net";
             options.RequireHttpsMetadata = false;
             options.ClientId = "shipmentagent";
             options.ClientSecret = "shipment";
             options.ResponseType = "code id_token";
             options.SaveTokens = true;
             options.GetClaimsFromUserInfoEndpoint = true;
               // options.Scope.Add("scope_used_for_hybrid_flow");
               options.Scope.Add("openid");
             options.Scope.Add("email");
             options.Scope.Add("profile");
             options.Scope.Add("productapi");
             options.Scope.Add("orderapi");
             options.Scope.Add("paymentapi");
               // options.Scope.Add("shipmentapi");
               //     options.Scope.Add("role");
               //  options.Scope.Add("customProfile");
               options.Scope.Add("offline_access");
             options.SaveTokens = true;
         });


            services.AddAuthorization();

            services.AddDbContext<ShipmentAppContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ShipmentAppContext")));
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
        }
    }
}

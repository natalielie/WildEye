using EcoClean.Data;
using EcoClean.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace EcoClean
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
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();


            

            //services.AddIdentityServer().AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            //       services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //        .AddJwtBearer(options =>
            //        {
            //            options.RequireHttpsMetadata = false;
            //            options.TokenValidationParameters = new
            //TokenValidationParameters
            //            {
            //                ValidateIssuer = true,
            //                ValidIssuer = AuthOptions.Issuer,
            //                ValidateAudience = true,
            //                ValidAudience =
            //AuthOptions.Audience,
            //                ValidateLifetime = true,
            //                IssuerSigningKey =
            //AuthOptions.GetSymmetricSecurityKey(),
            //                ValidateIssuerSigningKey = true
            //            };
            //        }).AddIdentityServerJwt()
            //        .AddCookie();


            ///////////////////////
            ///


            //    services.Configure<JwtBearerOptions>(
            //IdentityServerJwtConstants.IdentityServerJwtBearerScheme,
            //options =>
            //{
            //    var onTokenValidated = options.Events.OnTokenValidated;

            //    options.Events.OnTokenValidated = async context =>
            //    {
            //        await onTokenValidated(context);
            //    };
            //});

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            //    services.AddAuthorization(options =>
            //    {
            //        options.AddPolicy("IsAdmin",
            //            policy =>
            //            {
            //                // Even though we are using JwtClaimTypes in the ProfileService of the IdentityServer
            //                // the actual user claims are converted to those in ClaimTypes so check for them here
            //                policy.RequireClaim(ClaimTypes.Role, "administrator");
            //            });
            //    });


            //services.Configure<IdentityOptions>(options =>
            //{
            //    // Password settings.
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequiredUniqueChars = 1;

            //    // Lockout settings.
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //    options.Lockout.MaxFailedAccessAttempts = 5;
            //    options.Lockout.AllowedForNewUsers = true;

            //    // User settings.
            //    options.User.AllowedUserNameCharacters =
            //    "àabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" +
            //    "àáâãäå¸æçèéêëìíîïðñòóôõö÷øùúûüýþÿº¿ÀÁÂÃÄÅ¨ÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßª¯" +
            //    "0123456789-._@+";
            //    options.User.RequireUniqueEmail = false;
            //});

            //services.AddControllers().AddNewtonsoftJson();

            services.AddMvc();


            services.AddControllersWithViews();
            services.AddRazorPages();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });


        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsLocalDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseDeveloperExceptionPage();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors(options => options.AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/ClientApp/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsLocalDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
                else{
                    app.UseExceptionHandler("/error");

                }
            });


        }
        
    }
}

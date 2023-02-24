using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using MobaSpace.Core.Data;
using MobaSpace.Core.Data.Api;
using MobaSpace.Core.Data.Datalayers;
using MobaSpace.Core.Data.Models;
using MobaSpace.Core.Email;
using MobaSpace.Core.Utils;
using MobaSpace.Web.UI.Models;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;

namespace MobaSpace.Web.UI
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
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZËÍÈ‡‚ÓÙ-_Áè123456789";

            });
            services.AddDbContext<MobaDbContext>(options => {
                options.UseNpgsql(Configuration.GetConnectionString("Context"), 
                                    x => x.MigrationsHistoryTable("_MigrationsHistory", "mobaspace_data"));
            });
            services.AddScoped<MobaDataLayer>();
            services.AddControllersWithViews();
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.Configure<EmailSettings>(Configuration.GetSection("Email"));
            services.AddSingleton<EmailSender>();
            services.Configure<WebServerSettings>(Configuration.GetSection("WebServer"));
            services.Configure<List<ApiSettings>>(Configuration.GetSection("Apis"));
            services.AddAuthentication()
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = Configuration["Apis:Google:ClientId"];
                options.ClientSecret = Configuration["Apis:Google:ClientSecret"];
            })
            .AddWithings(Configuration["Apis:Withings:ClientId"],
            Configuration["Apis:Withings:ClientSecret"],
            Configuration["Apis:Withings:CallbackPath"],
            Configuration["Apis:Withings:AuthorizeEndPoint"],
            Configuration["Apis:Withings:TokenEndPoint"],
            Configuration["Apis:Withings:Scope"],
            Configuration["WebServer:Uri"])
            //.AddFitbit(Configuration["Apis:Fitbit:ClientId"], Configuration["Apis:Fitbit:ClientSecret"], Configuration["Apis:Fitbit:CallbackPath"])
            ;
            services.AddHttpClient();
            services.AddSession(option =>
            {
                option.Cookie.HttpOnly = true;
                option.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            services.AddHostedService<WithingsApiService>();

            IMvcBuilder builder = services.AddRazorPages().AddRazorPagesOptions(option =>
            {
                option.Conventions.AddPageRoute("/Alarme/Index", "");
            }
        );



#if DEBUG
            builder.AddRazorRuntimeCompilation();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MobaDbContext context, IServiceProvider serviceProvider, UserManager<User> userManager, MobaDataLayer layer)
        {

            app.UseForwardedHeaders();

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

            if (Configuration["WebServer:Uri"].Contains("https"))
            {
                app.UseHttpsRedirection();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            context.Database.Migrate();
            CreateRoles(serviceProvider).Wait();
            CreateDefaultAdmin(layer, userManager).Wait();


        }

        private async Task CreateDefaultAdmin(MobaDataLayer layer, UserManager<User> userManager)
        {
            List<User> existingUsers = await layer.GetAllUsersAsync();
            if (existingUsers.Count == 0)
            {
                var user = new User { UserName = "Admin", Email = "admin@mobaspace.com", EmailConfirmed = true, Creation = DateTime.UtcNow, LastConnection = DateTime.UtcNow, PasswordHash = "AQAAAAEAACcQAAAAEHB39/SU+H/79oHQ67Hva8SrdahC72Ed7SDhanIgASSEtEW8gQ0DE8GPs0Ui2TjTDA==" };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "ADMINISTRATEUR");
                }
            }
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var roleName in Roles.GetRoles())
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await RoleManager.CreateAsync(new IdentityRole(roleName));
                }

            }
            var roleResponsable = await RoleManager.RoleExistsAsync("Responsable");
            if (roleResponsable)
            {
                await RoleManager.DeleteAsync(await RoleManager.FindByNameAsync("Responsable"));
            }

        }
    }
}

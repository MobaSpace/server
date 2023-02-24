using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobaSpace.Core.Data;
using MobaSpace.Core.Data.Models;

[assembly: HostingStartup(typeof(MobaSpace.Web.UI.Areas.Identity.IdentityHostingStartup))]
namespace MobaSpace.Web.UI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDefaultIdentity<User>(options => 
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.User.AllowedUserNameCharacters += " ";
                }
                )
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MobaDbContext>();
            });
        }
    }
}
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Snackis_Forum_.Areas.Identity.Data;
using Snackis_Forum_.Data;

[assembly: HostingStartup(typeof(Snackis_Forum_.Areas.Identity.IdentityHostingStartup))]
namespace Snackis_Forum_.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Snackis_Forum_Context>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Snackis_Forum_ContextConnection")));

                services.AddDefaultIdentity<ForumUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<Snackis_Forum_Context>();
            });
        }
    }
}
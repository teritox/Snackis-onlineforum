using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snackis_Forum_.Data;
using Snackis_Forum_.Services;
using Microsoft.AspNetCore.Identity;
using Snackis_Forum_.Gateway;
using System.Globalization;

namespace Snackis_Forum_
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
            services.AddHttpClient<Gateway.FulaOrdGateway>();
            services.AddScoped<IForumDataService, ForumDataService>();
            services.AddScoped<IFulaOrdGateway, FulaOrdGateway>();

            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/SiteAdmin", "RequireAdministratorRole");
                options.Conventions.AuthorizePage("/MessageOpen");
                options.Conventions.AuthorizePage("/NewMessage");
                options.Conventions.AuthorizePage("/PrivateMessages");
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                     policy => policy.RequireRole("Administrator"));
            });

            //For cookieconsent
            services.Configure<CookiePolicyOptions>(option =>
            {
                option.CheckConsentNeeded = conesent => true;
                option.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            services.AddControllers();

            services.AddDbContext<ForumContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ForumContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Cultureinfo set to swedish so DateTime gets date and time in YYYY/MM/DD with 24 hour HH:MM.
            var cultureInfo = new CultureInfo("sv");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}

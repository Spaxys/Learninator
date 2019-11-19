using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learninator.Database;
using Learninator.Repositories;
using Learninator.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Learninator
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<LearninatorContext>(opts =>
            opts.UseSqlServer(Configuration["ConnectionStrings:LearninatorContext"]),
                ServiceLifetime.Transient);

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<LearninatorContext>();

            services.Configure<IdentityOptions>(options =>
           {
               var passwordPolicies = Configuration.GetSection("IdentitySettings:PasswordPolicy");
               if (passwordPolicies != null)
               {
                   options.Password.RequiredLength = int.Parse(passwordPolicies["RequiredLength"]);
                   options.Password.RequiredUniqueChars = int.Parse(passwordPolicies["RequiredUniqueChars"]);
                   options.Password.RequireDigit = bool.Parse(passwordPolicies["RequireDigit"]);
                   options.Password.RequireLowercase = bool.Parse(passwordPolicies["RequireLowercase"]);
                   options.Password.RequireUppercase = bool.Parse(passwordPolicies["RequireUppercase"]);
                   options.Password.RequireNonAlphanumeric = bool.Parse(passwordPolicies["RequireNonAlphanumeric"]);
               }
           });

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<ITagsRepository, TagsRepository>();
            services.AddScoped<ILinksRepository, LinksRepository>();
            services.AddScoped<ILinksService, LinksService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

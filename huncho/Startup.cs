using huncho.Data;
using huncho.Data.Seeders;
using huncho.Extensions;
using huncho.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace huncho
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

            services.AddDbContext<HunchoDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<HunchoDbContext>();

            services.AddTransient<DbContext, HunchoDbContext>();
            services.RegisterRepositories();

            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: null,
                //    template: "{category}/Page{page:int}",
                //    defaults: new { Controller = "Product", Action = "Index" });

                //routes.MapRoute(
                //    name: null,
                //    template: "Page{page:int}",
                //    defaults: new { Controller = "Product", Action = "Index", page = 1 });

                //routes.MapRoute(
                //    name: null,
                //    template: "{category}",
                //    defaults: new { Controller = "Product", Action = "Index", page = 1 });

                //routes.MapRoute(
                //    name: null,
                //    template: "",
                //    defaults: new { Controller = "Product", Action = "Index", category = "", page = 1 });

                routes.MapRoute(
                    name: "default", 
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });

            SeedData.EnsurePopulated(app);
        }
    }
}

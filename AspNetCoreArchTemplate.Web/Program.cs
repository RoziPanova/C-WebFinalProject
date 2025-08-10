namespace AspNetCoreArchTemplate.Web
{
    using AspNetCoreArchTemplate.Data;
    using AspNetCoreArchTemplate.Data.Models;
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;
    using AspNetCoreArchTemplate.Data.Seeding;
    using AspNetCoreArchTemplate.Data.Seeding.Interfaces;
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services
                .AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddRepositories(typeof(IProductRepository).Assembly);
            builder.Services.AddUserDefinedServices(typeof(IProductService).Assembly);

            builder.Services.AddTransient<IIdentitySeeder, IdentitySeeder>();

            builder.Services.AddControllersWithViews(options
                => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));


            WebApplication? app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=604800");
                }
            });

            app.UseRouting();

            app.SeedDefaultIdentity();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UserAdminRedirection();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}

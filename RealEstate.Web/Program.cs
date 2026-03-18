using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Services;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Repositories;

namespace RealEstate.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "The connection string 'DefaultConnection' is not configured. " +
                    "Please define 'ConnectionStrings:DefaultConnection' in configuration.");
            }

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IInvestmentRepository, InvestmentRepository>();
            builder.Services.AddScoped<InvestmentService>();
            builder.Services.AddScoped<IWalletRepository, WalletRepository>();
            builder.Services.AddScoped<WalletService>();
            builder.Services.AddScoped<ISaleListingRepository, SaleListingRepository>();
            builder.Services.AddScoped<SaleListingService>();
            builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
            builder.Services.AddScoped<PropertyService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IKycRepository, KycRepository>();
            builder.Services.AddScoped<KycService>();
            builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<AppDbContext>();
                await context.Database.MigrateAsync();

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
                var userManager = services.GetRequiredService<UserManager<User>>();

                string[] roleNames = { "Admin", "User", "Guest" };
                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                        await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                }

                var admin = await userManager.FindByNameAsync("admin");
                if (admin == null)
                {
                    var adminUser = new User
                    {
                        UserName = "admin",
                        Email = "admin@propvest.com",
                        FirstName = "Admin",
                        LastName = "PropVest",
                        Role = "Admin",
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    };
                    var result = await userManager.CreateAsync(adminUser, "Admin@123");
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Services;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "The connection string 'DefaultConnection' is not configured. " +
                    "Please define 'ConnectionStrings:DefaultConnection' in configuration (e.g., appsettings.json or environment variables).");
            }

            builder.Services.AddDbContext<RealEstate.Infrastructure.Data.AppDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddScoped<IInvestmentRepository, InvestmentRepository>();

            builder.Services.AddScoped<InvestmentService>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

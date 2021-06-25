  
using System;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistance;
using Persistence;

namespace API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scoped = host.Services.CreateScope();
            var services = scoped.ServiceProvider;
            try
            {
                var dbContext = services.GetRequiredService<DataContext>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await dbContext.Database.MigrateAsync();
                await Seed.SeedData(dbContext, userManager);
            }
            catch (System.Exception ex)
            {
                //log the error
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError("Error: ", ex.Message);
            }
            
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

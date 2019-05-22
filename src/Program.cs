using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RuhRoh.Samples.WebAPI.Data;

namespace RuhRoh.Samples.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webhost = BuildWebHost(args);
            await MigrateDatabase(webhost.Services);

            await webhost.RunAsync();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        private static async Task MigrateDatabase(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbc = scope.ServiceProvider.GetService<TodoDbContext>();
                await dbc.Database.MigrateAsync();
            }
        }
    }
}

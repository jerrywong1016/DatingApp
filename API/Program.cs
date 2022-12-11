using API.Data;
using API.Entities;
using API.Extensions;
using API.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
// //add service to the container
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {   
            // var builder = WebApplication.CreateBuilder(args);
            // builder.Services.AddControllers();
            // builder.Services.AddApplicationServices(builder.Configuration);
            // builder.Services.AddIdentityServices(builder.Configuration);

            // var app = builder.Build();

            // app.UseMiddleware<ExceptionMiddleware>();
            // app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
            //     .WithOrigins("http://localhost:4200"));

            // app.UseAuthentication();
            // app.UseAuthorization();

            // app.MapControllers();

            // using var scope = app.Services.CreateScope();
            // var services = scope.ServiceProvider;

            // try{
            //     var context = services.GetRequiredService<DataContext>();
            //     await context.Database.MigrateAsync();
            //     await Seed.SeedUsers(context);
            // }
            // catch(Exception ex){
            //     var logger = services.GetService<ILogger<Program>>();
            //     logger.LogError(ex, "An error occurred during migration");
            // }

            // app.Run();
            CreateHostBuilder(args).Build().Run();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
                .WithOrigins("http://localhost:4200"));

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try{
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
                await Seed.SeedUsers(context);
            }
            catch(Exception ex){
                var logger = services.GetService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during migration");
            }

            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
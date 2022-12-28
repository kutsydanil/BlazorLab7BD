using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using CinemaCore;
using WebApplicationLab6.Middlewares;

namespace WebApplicationLab6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<CinemaContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            using (ServiceProvider serviceProvider = builder.Services.BuildServiceProvider())
            {
                var context = serviceProvider.GetRequiredService<CinemaContext>();
                DbInitializer.Initialize(context);
            }

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<AuthMidleware>();

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
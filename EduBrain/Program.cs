using EduBrain.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace EduBrain
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<EduBrainContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200") // Allow your frontend origin
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            // Add controllers and handle JSON options
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.MaxDepth = 32;  // Optional depth control
                });

            // Add Authorization services (Required)
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Detailed error messages in development mode
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error"); // Custom error page for production
                app.UseHsts();  // Use HTTP Strict Transport Security in production
            }

            app.UseHttpsRedirection();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            // Use CORS before routing
            app.UseCors("AllowAllOrigins");

            // Add Authorization Middleware (Required)
            app.UseAuthorization();

            // Add routing and controllers
            app.MapControllers();

            app.Run();
        }
    }
}

using BLL.Interface;
using BLL.Service;
using Microsoft.EntityFrameworkCore;
using DAL.Data;


namespace Garena2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<GarenaContext>(options =>
              options.UseMySql(
              connectionString,
              ServerVersion.AutoDetect(connectionString)   // Auto detect phiên bản MySQL
          )
      );


            builder.Services.AddScoped<RegisterInterface, RegisterService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOriginPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAnyOriginPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
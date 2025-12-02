using BLL.Interface;
using BLL.Service;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Garena2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Lấy connection string ưu tiên environment variable (Railway)
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                                   ?? builder.Configuration.GetConnectionString("DefaultConnection");

            var connBuilder = new MySqlConnectionStringBuilder(connectionString)
            {
                SslMode = MySqlSslMode.None
            };

            // Test MySQL connection ngay khi start
            try
            {
                using var testConn = new MySqlConnection(connBuilder.ConnectionString);
                await testConn.OpenAsync();
                Console.WriteLine("✅ MySQL connected successfully!");
                await testConn.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ MySQL connection failed: " + ex.Message);
            }

            // DbContext
            builder.Services.AddDbContext<GarenaContext>(options =>
                options.UseMySql(connBuilder.ConnectionString, ServerVersion.AutoDetect(connBuilder.ConnectionString))
                       .EnableSensitiveDataLogging()
                       .EnableDetailedErrors()
            );

            // DI: interface -> service
            builder.Services.AddScoped<RegisterInterface, RegisterService>();

            // Controllers + Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Bật Swagger cho mọi môi trường
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Garena API V1");
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

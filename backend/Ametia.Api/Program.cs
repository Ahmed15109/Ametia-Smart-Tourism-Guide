using Grad.Models;
using Grad.Repo;
using Grad.Repo.Base;
using Microsoft.EntityFrameworkCore;

namespace Grad
{
    public class Program
    {
        private const string FrontendCorsPolicy = "FrontendClients";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("MyConnection");
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException(
                        "ConnectionStrings:MyConnection is not configured. Use .NET User Secrets locally or an environment variable in deployment.");
                }

                options.UseSqlServer(connectionString);
            });

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                var idleTimeoutMinutes = builder.Configuration.GetValue<int?>("Session:IdleTimeoutMinutes") ?? 30;
                options.IdleTimeout = TimeSpan.FromMinutes(idleTimeoutMinutes);
                options.Cookie.Name = ".Ametia.Session";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            builder.Services.AddTransient(typeof(IRepoBase<>), typeof(MainRepo<>));

            var allowedOrigins = builder.Configuration
                .GetSection("Cors:AllowedOrigins")
                .Get<string[]>() ?? [];

            if (allowedOrigins.Length == 0)
            {
                throw new InvalidOperationException("At least one CORS origin must be configured in Cors:AllowedOrigins.");
            }

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(FrontendCorsPolicy, policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(FrontendCorsPolicy);
            app.UseSession();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

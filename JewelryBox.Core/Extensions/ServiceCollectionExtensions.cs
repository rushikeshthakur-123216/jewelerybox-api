using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JewelryBox.Application.Interfaces;
using JewelryBox.Application.Services;
using JewelryBox.Core.Configuration;
using JewelryBox.Domain.Interfaces;
using JewelryBox.Infrastructure.Data;
using JewelryBox.Infrastructure.Repositories;
using JewelryBox.Infrastructure.Services;

namespace JewelryBox.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJewelryBoxServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuration
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

            // Database
            var dbSettings = configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
            services.AddSingleton<IDbConnectionFactory>(new PostgresConnectionFactory(dbSettings?.ConnectionString ?? string.Empty));

            // Services
            services.AddSingleton<IQueryService, XmlQueryService>();
            services.AddScoped<IUserRepository, UserRepository>();

            // JWT Service
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            services.AddSingleton(new JwtService(
                jwtSettings?.SecretKey ?? "your-secret-key-here",
                jwtSettings?.Issuer ?? "JewelryBox",
                jwtSettings?.Audience ?? "JewelryBoxUsers",
                jwtSettings?.ExpirationMinutes ?? 60
            ));

            // Application Services
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings?.SecretKey ?? "your-secret-key-here");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings?.Issuer ?? "JewelryBox",
                    ValidateAudience = true,
                    ValidAudience = jwtSettings?.Audience ?? "JewelryBoxUsers",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}

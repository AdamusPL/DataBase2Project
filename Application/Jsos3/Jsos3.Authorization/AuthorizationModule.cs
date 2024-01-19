using Jsos3.Authorization.Infrastructure.Repository;
using Jsos3.Authorization.Models;
using Jsos3.Authorization.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jsos3.Authorization;

public static class AuthorizationModule
{
    public static void AddAuthorizationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["AuthToken"];
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<SecurityTokenHandler, JwtSecurityTokenHandler>();
        services.AddScoped<IFieldsOfStudiesRepository, FieldsOfStudiesRepository>();
        services.AddScoped<PasswordHasher<User>>();
    }
}
// IdentityServiceExtensions.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Import necessary ASP.NET Core Identity and JWT authentication packages
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace skillz_backend.Extensions
{
    public static class IdentityServiceExtensions
    {
        // Extension method to add identity services with JWT authentication
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            // Configure JWT authentication with options
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Validate the issuer signing key
                        ValidateIssuerSigningKey = true,
                        
                        // Set the issuer signing key using the configured token key
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        
                        // Disable issuer validation
                        ValidateIssuer = false,
                        
                        // Disable audience validation
                        ValidateAudience = false,
                    };
                });

            return services;
        }
    }
}

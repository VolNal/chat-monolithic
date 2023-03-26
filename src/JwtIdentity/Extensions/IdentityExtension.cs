using JwtIdentity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JwtIdentity.Extensions;

public static class IdentityExtension
{
    public static IServiceCollection AddJwtIdentity(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptionsService.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptionsService.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptionsService.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });

        return services;
    }
}
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BarterProject.Security;

public static class AuthenticationDependency
{
    public static IServiceCollection AddAuthenticationDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(opts =>
        {
            opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;

            cfg.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!)),
                ValidAudience = configuration["JWT:ValidIssuer"],
                //ValidateIssuer = false,
                //ValidateIssuerSigningKey = false,
                //ValidateAudience = false,
            };
            cfg.IncludeErrorDetails = true;
        });

        return services;

    }
}

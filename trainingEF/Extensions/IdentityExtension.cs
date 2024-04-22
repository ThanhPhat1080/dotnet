using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using trainingEF.Data;
using trainingEF.Models.DTOs;

namespace trainingEF.Extensions;

public static class IdentityExtension
{
    public static IServiceCollection AddAuthentication(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddAuthentication(configureOptions =>
        {
            // Building the header, header will sending Authorization
            configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            configureOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwt =>
        {
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Secret"])),

                ValidateIssuer = false, // for development
                ValidateAudience = false, // for development
                RequireExpirationTime = false, // for development, need to be update when refresh token issue

                ValidateLifetime = true
            };
        });

        service
            .AddIdentityCore<UserDto>()
            .AddUserManager<UserManager<UserDto>>()
            .AddRoles<IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<AppDbContext>();

        return service;
    }
}

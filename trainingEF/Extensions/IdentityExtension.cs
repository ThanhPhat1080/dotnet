using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Text;
using trainingEF.Data;

namespace trainingEF.Extensions;

public static class IdentityExtension
{
    public static IServiceCollection AddAuthentication(this IServiceCollection service, IConfiguration configuration)
    {
        _ = service.AddAuthentication(configureOptions =>
        {
            // Building the header, header will sending Authorization
            configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            configureOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwt =>
        {
#pragma warning disable CS8604 // Possible null reference argument.
            string keySecret = configuration.GetSection(key: "JwtConfig:Secret").ToString();
            byte[] key = Encoding.ASCII.GetBytes(keySecret);
            #pragma warning restore CS8604 // Possible null reference argument.

            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),

                ValidateIssuer = false, // for development
                ValidateAudience = false, // for development
                RequireExpirationTime = false, // for development, need to be update when refresh token issue

                ValidateLifetime = true,
            };
        });

        service.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedEmail = false)
            .AddEntityFrameworkStores<AppDbContext>();

        return service;
    }
}

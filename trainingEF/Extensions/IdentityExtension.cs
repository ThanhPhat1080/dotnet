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
            string keySecret = configuration.GetSection("JwtConfig:Secret").Value.ToString();
            byte[] key = Encoding.UTF8.GetBytes(keySecret);

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

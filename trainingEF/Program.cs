using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using trainingEF.Configuration;
using trainingEF.Data;
using trainingEF.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using trainingEF.Models.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#pragma warning disable CS0618 // Type or member is obsolete
builder.Services.AddControllers()
    .AddFluentValidation(df =>
    {
        df.RegisterValidatorsFromAssemblyContaining<UserRegistrationRequestValidator>();
    }
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});


// Config -- not working
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(key: "JwtConfig"));

// Updating middleware. There's going to be an Authentication. Any request with header with be validate
builder.Services.AddAuthentication(configureOptions =>
{
    // Building the header, header will sending Authorization
    configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    byte[] key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection(key: "JwtConfig:Secret").ToString());

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


// DI repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedEmail = false)
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

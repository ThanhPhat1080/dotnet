using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using trainingEF.Configuration;
using trainingEF.Data;
using trainingEF.Repositories;
using FluentValidation.AspNetCore;
using trainingEF.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddFluentValidation();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);

// DI repositories
builder.Services.AddAppRepositoryDependency();

// Config -- not working
//builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(key: "JwtConfig"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}



# region Seed / Update DB
using IServiceScope scope = app.Services.CreateScope();
IServiceProvider serviceProvider = scope.ServiceProvider;

// Migrate the database
try
{
    AppDbContext appDb = serviceProvider.GetRequiredService<AppDbContext>();
    await appDb.Database.MigrateAsync();

    // Add the roles
    RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await AppDbSeeding.SeedingData(roleManager: roleManager);
} catch(Exception e)
{
    Console.Write("{@Exception}", e);
}
#endregion

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

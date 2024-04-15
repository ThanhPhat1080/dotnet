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

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

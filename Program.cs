using Employee.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add Repositories Dependency Injection
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();




var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

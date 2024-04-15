using Microsoft.EntityFrameworkCore;
using trainingEF.Data;

namespace trainingEF.Extensions;

public static class DbContextExtension
{
    public static IServiceCollection AddDbContext(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("Default"));
        });

        return service;
    }
}

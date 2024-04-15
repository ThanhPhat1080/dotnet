using System.Runtime.CompilerServices;

namespace trainingEF.Repositories;

public static class AppRepositoriesDependency
{
    public static IServiceCollection AddAppRepositoryDependency(this IServiceCollection service)
    {
        service.AddScoped<IUserRepository, UserRepository>();

        return service;
    }
}

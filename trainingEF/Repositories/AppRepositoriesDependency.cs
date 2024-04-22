namespace trainingEF.Repositories;

public static class AppRepositoriesDependency
{
    public static IServiceCollection AddAppRepositoryDependency(this IServiceCollection service)
    {
        service.AddScoped<IIdentityRepository, IdentityRepository>();
        service.AddScoped<IOrderRepository, OrderRepository>();

        return service;
    }
}

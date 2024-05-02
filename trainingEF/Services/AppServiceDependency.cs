using trainingEF.Repositories;

namespace trainingEF.Services;

public static class AppServiceDependency
{
    public static IServiceCollection AddAppServiceDependency(this IServiceCollection service)
    {
        service.AddScoped<IProductService, ProductServices>();

        return service;
    }
}

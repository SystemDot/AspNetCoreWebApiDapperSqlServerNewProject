using NewProject.WebApi.Domain;
using NewProject.WebApi.Infrastructure.Dapper;

namespace NewProject.WebApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDbConnectionFactory, SqlServerDbConnectionFactory>();
        serviceCollection.AddSingleton<IRepository<NewProjectThing>, NewProjectThingDapperRepository>();

        return serviceCollection;
    }
}
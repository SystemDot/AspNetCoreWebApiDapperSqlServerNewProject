using NewProject.Domain;
using NewProject.Infrastructure.Dapper.SqlServer;

namespace NewProject.WebApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        serviceCollection.AddSingleton<IRepository<NewProjectThing>, ThingRepository>();

        return serviceCollection;
    }
}
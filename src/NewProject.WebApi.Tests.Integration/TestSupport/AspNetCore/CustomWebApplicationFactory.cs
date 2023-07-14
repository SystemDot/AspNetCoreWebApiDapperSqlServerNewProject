using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NewProject.Infrastructure.Dapper.SqlServer;
using NewProject.WebApi.Tests.Integration.TestSupport.AdoNet;

namespace NewProject.WebApi.Tests.Integration.TestSupport.AspNetCore;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(services =>
                services
                    .AddSingleton<IDbConnectionFactory, TestDbConnectionFactory>());
    }
}
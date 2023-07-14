using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NewProject.WebApi.Infrastructure.Dapper;
using NewProject.WebApi.Tests.Integration.TestSupport.FakeAdoNet;

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
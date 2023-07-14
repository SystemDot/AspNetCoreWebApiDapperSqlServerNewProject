using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using NewProject.WebApi.Infrastructure.Dapper;
using NewProject.WebApi.Tests.Integration.TestSupport.AdoNet;
using NewProject.WebApi.Tests.Integration.TestSupport.XUnit;

namespace NewProject.WebApi.Tests.Integration.TestSupport;

[Collection(nameof(IntegrationTestsCollection))]
public abstract class TestBase : IDisposable
{
    protected readonly IntegrationTestsFixture Fixture;
    protected readonly HttpClient HttpClient;
    protected HttpResponseMessage HttpResponseMessage;
    protected readonly TestDbConnectionFactory TestDbConnectionFactory;
    protected IServiceScope ServiceScope;
    protected Fixture AutoFixture = new();

    protected TestBase(IntegrationTestsFixture fixture)
    {
        Fixture = fixture;
        HttpClient = Fixture.WebApplicationFactory.CreateClient();
        ServiceScope = Fixture.WebApplicationFactory.Services.CreateScope();
        TestDbConnectionFactory = ServiceScope.ServiceProvider.GetServices<IDbConnectionFactory>().OfType<TestDbConnectionFactory>().First();
    }

    public void Dispose()
    {
        HttpClient?.Dispose();
        HttpResponseMessage?.Dispose();
        ServiceScope?.Dispose();
    }
}
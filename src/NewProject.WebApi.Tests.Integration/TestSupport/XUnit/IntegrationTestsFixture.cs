using Microsoft.AspNetCore.Mvc.Testing;
using NewProject.WebApi.Tests.Integration.TestSupport.AspNetCore;

namespace NewProject.WebApi.Tests.Integration.TestSupport.XUnit;

public class IntegrationTestsFixture : IAsyncLifetime
{
    public readonly WebApplicationFactory<Program> WebApplicationFactory;

    public IntegrationTestsFixture()
    {
        WebApplicationFactory = new CustomWebApplicationFactory();
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await WebApplicationFactory.DisposeAsync();
    }
}
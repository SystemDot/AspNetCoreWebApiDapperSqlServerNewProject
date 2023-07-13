using System.Net;
using AutoFixture;
using FluentAssertions;

namespace NewProject.WebApi.Tests.Integration.NewProjectThing;

public class NewProjectThingGetTests : TestBase
{
    private readonly string _id;

    public NewProjectThingGetTests(IntegrationTestsFixture fixture) : base(fixture)
    {
        _id = AutoFixture.Create<string>();
    }

    [Fact]
    public async Task Given_UnknownId_When_Get_Then_ShouldRespond404()
    {
        HttpResponseMessage = await HttpClient.GetAsync($"NewProjectThing/{_id}");

        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Given_ValidRequest_When_Get_Then_ShouldExecuteSqlCommand()
    {
        HttpResponseMessage = await HttpClient.GetAsync($"NewProjectThing/{_id}");

        HttpResponseMessage.EnsureSuccessStatusCode();
        TestDbConnectionFactory.DbConnection.DbCommand.CommandText.Should().Be("UpsertNewProjectThing");
        TestDbConnectionFactory.DbConnection.DbParameterCollection.DbParameters.Should().ContainSingle(p => p.ParameterName == "Id" && _id.Equals(p.Value));
    }
}
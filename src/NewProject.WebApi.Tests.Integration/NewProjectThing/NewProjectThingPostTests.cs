using System.Net.Http.Json;
using AutoFixture;
using FluentAssertions;
using NewProject.WebApi.Tests.Integration.TestSupport;
using NewProject.WebApi.Tests.Integration.TestSupport.XUnit;

namespace NewProject.WebApi.Tests.Integration.NewProjectThing;

public class NewProjectThingPostTests : TestBase
{
    private readonly string _id;
    private readonly NewProjectThingRequestModel _model;

    public NewProjectThingPostTests(IntegrationTestsFixture fixture) : base(fixture)
    {
        _id = AutoFixture.Create<string>();
        _model = AutoFixture.Create<NewProjectThingRequestModel>();
    }

    [Fact]
    public async Task Given_ValidRequest_When_Post_Then_ShouldExecuteSqlCommand()
    {
        HttpResponseMessage = await HttpClient.PostAsJsonAsync($"NewProjectThing/{_id}", _model);

        HttpResponseMessage.EnsureSuccessStatusCode();
        var dbCommand = TestDbConnectionFactory.DbConnection.DbCommands.Last();
        dbCommand.CommandText.Should().Be("UpsertNewProjectThing");
        dbCommand.DbParameters.Should().ContainSingle(p => p.ParameterName == "Id" && _id.Equals(p.Value));
        dbCommand.DbParameters.Should().ContainSingle(p => p.ParameterName == "TheThing" && _model.TheThing.Equals(p.Value));
    }
}
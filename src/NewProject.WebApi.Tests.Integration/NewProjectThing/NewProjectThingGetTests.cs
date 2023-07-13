using System.Net;
using AutoFixture;
using FluentAssertions;
using NewProject.WebApi.Controllers;
using Newtonsoft.Json;

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
        TestDbConnectionFactory.DbConnection.DataTable.Columns.Add("Id");
        TestDbConnectionFactory.DbConnection.DataTable.Columns.Add("TheThing");
        TestDbConnectionFactory.DbConnection.DataTable.Rows.Add(_id, "The Thing");

        HttpResponseMessage = await HttpClient.GetAsync($"NewProjectThing/{_id}");

        HttpResponseMessage.EnsureSuccessStatusCode();
        TestDbConnectionFactory.DbConnection.DbCommand.CommandText.Should().Be("SelectNewProjectThing");
        TestDbConnectionFactory.DbConnection.DbParameterCollection.DbParameters.Should().ContainSingle(p => p.ParameterName == "Id" && _id.Equals(p.Value));
        HttpResponseMessage.Content.As<NewProjectThingResponseModel>().Id.Should().Be(_id);
        HttpResponseMessage.Content.As<NewProjectThingResponseModel>().TheThing.Should().Be("The Thing");
    }
}

public static class HttpContentExtensions
{
    public static TModel As<TModel>(this HttpContent httpContent)
    {
        return JsonConvert.DeserializeObject<TModel>(httpContent.ReadAsStringAsync().GetAwaiter().GetResult());
    }
}

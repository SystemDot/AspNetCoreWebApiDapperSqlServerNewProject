using NewProject.Domain;
using NewProject.WebApi.Controllers;

namespace NewProject.WebApi;

public static class NewProjectThingExtensions
{
    public static NewProjectThingResponseModel MapToResponseModel(this NewProjectThing entity)
    {
        return new()
        {
            Id = entity.Id,
            TheThing = entity.TheThing
        };
    }
}
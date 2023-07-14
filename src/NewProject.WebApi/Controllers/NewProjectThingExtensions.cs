using NewProject.WebApi.Domain;

namespace NewProject.WebApi.Controllers;

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
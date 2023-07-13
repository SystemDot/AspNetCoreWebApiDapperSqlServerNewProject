using NewProject.Domain;

namespace NewProject.WebApi;

public static class NewProjectThingRequestModelExtensions
{
    public static NewProjectThing MapToEntity(this NewProjectThingRequestModel requestModel)
    {
        return new()
        {
            Id = requestModel.Id,
            TheThing = requestModel.TheThing
        };
    }
}
using Newtonsoft.Json;

namespace NewProject.WebApi.Tests.Integration.NewProjectThing;

public static class HttpContentExtensions
{
    public static TModel As<TModel>(this HttpContent httpContent)
    {
        return JsonConvert.DeserializeObject<TModel>(httpContent.ReadAsStringAsync().GetAwaiter().GetResult());
    }
}
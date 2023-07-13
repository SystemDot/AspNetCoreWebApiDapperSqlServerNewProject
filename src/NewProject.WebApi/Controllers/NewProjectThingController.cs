using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NewProject.Domain;

namespace NewProject.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewProjectThingController : ControllerBase
    {
        private readonly IRepository<NewProjectThing> _repository;

        public NewProjectThingController(IRepository<NewProjectThing> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Results<Ok<NewProjectThingResponseModel>, NotFound, BadRequest>> Get([FromRoute]string id)
        {
            var entity = await _repository.GetAsync(id);

            if (entity == default)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(entity.MapToResponseModel());
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<Results<CreatedAtRoute<string>, BadRequest>> Post(string id, NewProjectThingRequestModel model)
        {
            model.Id = id;
            await _repository.SaveAsync(model.MapToEntity());

            return TypedResults.CreatedAtRoute(model.Id, "");
        }
    }
}
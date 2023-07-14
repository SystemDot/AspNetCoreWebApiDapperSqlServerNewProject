using System.Data;
using Dapper;
using NewProject.WebApi.Domain;

namespace NewProject.WebApi.Infrastructure.Dapper
{
    public class NewProjectThingDapperRepository : IRepository<NewProjectThing>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public NewProjectThingDapperRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<NewProjectThing> GetAsync(string id)
        {
            using var connection = await _dbConnectionFactory.CreateAsync();
            return await connection
                .QueryFirstOrDefaultAsync<NewProjectThing>(
                    "SelectNewProjectThing",
                    new { Id = id},
                    commandType: CommandType.StoredProcedure);
        }

        public async Task SaveAsync(NewProjectThing entity)
        {
            using var connection = await _dbConnectionFactory.CreateAsync();
            await connection
                .ExecuteAsync(
                    "UpsertNewProjectThing",
                    new
                    {
                        entity.Id,
                        entity.TheThing
                    },
                    commandType: CommandType.StoredProcedure);
        }
    }
}

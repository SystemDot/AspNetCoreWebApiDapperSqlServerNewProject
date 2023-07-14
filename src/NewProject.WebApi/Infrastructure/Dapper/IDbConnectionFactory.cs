using System.Data;

namespace NewProject.WebApi.Infrastructure.Dapper;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync();
}
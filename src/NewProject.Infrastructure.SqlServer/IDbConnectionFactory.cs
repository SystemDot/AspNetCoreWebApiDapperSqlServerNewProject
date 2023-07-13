using System.Data;

namespace NewProject.Infrastructure.Dapper.SqlServer;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync();
}
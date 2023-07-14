using System.Data;
using System.Data.SqlClient;

namespace NewProject.WebApi.Infrastructure.Dapper;

public class SqlServerDbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlServerDbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IDbConnection> CreateAsync()
    {
        var connectionString = _configuration.GetSection("ConnectionStrings").GetChildren().First().Value;
        var sqlConnection = new SqlConnection(connectionString);
        await sqlConnection.OpenAsync();
        return sqlConnection;
    }
}
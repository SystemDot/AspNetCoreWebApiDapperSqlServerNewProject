using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace NewProject.Infrastructure.Dapper.SqlServer;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _configuration;

    public DbConnectionFactory(IConfiguration configuration)
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
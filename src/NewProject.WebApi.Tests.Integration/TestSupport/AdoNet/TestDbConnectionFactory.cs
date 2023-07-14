using System.Data;
using NewProject.Infrastructure.Dapper.SqlServer;

namespace NewProject.WebApi.Tests.Integration.TestSupport.AdoNet;

public class TestDbConnectionFactory : IDbConnectionFactory
{
    public TestDbConnection DbConnection = new();

    public Task<IDbConnection> CreateAsync()
    {
        return Task.FromResult(DbConnection as IDbConnection);
    }
}
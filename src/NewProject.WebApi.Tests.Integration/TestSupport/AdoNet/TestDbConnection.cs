using System.Data;
using System.Data.Common;

namespace NewProject.WebApi.Tests.Integration.TestSupport.AdoNet;

public class TestDbConnection : DbConnection, IDbConnection
{
    public TestDbCommand DbCommand;
    public TestDbParameterCollection DbParameterCollection;
    public DataTable DataTable = new();

    protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
    {
        throw new NotImplementedException();
    }

    public override void ChangeDatabase(string databaseName)
    {
    }

    public override void Close()
    {
    }

    public override void Open()
    {
    }

    public override string ConnectionString { get; set; }
    public override string Database { get; }
    public override ConnectionState State { get; }
    public override string DataSource { get; }
    public override string ServerVersion { get; }

    protected override DbCommand CreateDbCommand()
    {
        DbParameterCollection = new TestDbParameterCollection();
        DbCommand = new(this, DbParameterCollection, DataTable);
        return DbCommand;
    }
}
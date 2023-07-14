using System.Data;
using System.Data.Common;

namespace NewProject.WebApi.Tests.Integration.TestSupport.FakeAdoNet;

public class TestDbConnection : DbConnection, IDbConnection
{
    private readonly Func<IReadOnlyDictionary<string, DataTable>> _dataTables;
    private readonly List<TestDbCommand> _dbCommands = new();

    public IEnumerable<TestDbCommand> DbCommands => _dbCommands;

    public TestDbConnection(Func<IReadOnlyDictionary<string, DataTable>> dataTables)
    {
        _dataTables = dataTables;
    }

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
        _dbCommands.Add(new(this, _dataTables));
        return DbCommands.Last();
    }
}
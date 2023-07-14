using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace NewProject.WebApi.Tests.Integration.TestSupport.FakeAdoNet;

public class TestDbCommand : DbCommand, IDbCommand
{
    private readonly TestDbParameterCollection _testDbParameterCollection = new();
    private readonly Func<IReadOnlyDictionary<string, DataTable>> _dataTable;

    public TestDbCommand(
        DbConnection dbConnection,
        Func<IReadOnlyDictionary<string, DataTable>> dataTable)
    {
        _dataTable = dataTable;
        DbConnection = dbConnection;
    }
    public override void Cancel()
    {
    }

    public override int ExecuteNonQuery()
    {
        return 1;
    }

    public override object? ExecuteScalar()
    {
        return new { };
    }

    public override void Prepare()
    {
    }

    public override string CommandText { get; set; }
    public override int CommandTimeout { get; set; }
    public override CommandType CommandType { get; set; }
    public override UpdateRowSource UpdatedRowSource { get; set; }
    protected override DbConnection? DbConnection { get; set; }
    protected override DbParameterCollection DbParameterCollection => _testDbParameterCollection;
    protected override DbTransaction? DbTransaction { get; set; }
    public override bool DesignTimeVisible { get; set; }
    public List<DbParameter> DbParameters => _testDbParameterCollection.DbParameters;

    protected override DbParameter CreateDbParameter()
    {
        return new SqlParameter();
    }

    protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
    {
        if (!_dataTable().TryGetValue(CommandText, out var dataTable))
            throw new InvalidOperationException($"DataTable not setup for CommandText '{CommandText}'.");

        return new DataTableReader(dataTable);
    }
}
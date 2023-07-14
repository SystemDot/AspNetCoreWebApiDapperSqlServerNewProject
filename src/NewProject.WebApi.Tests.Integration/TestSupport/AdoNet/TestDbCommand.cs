using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace NewProject.WebApi.Tests.Integration.TestSupport.AdoNet;

public class TestDbCommand : DbCommand, IDbCommand
{
    private readonly DataTable _dataTable;

    public TestDbCommand(
        DbConnection dbConnection,
        TestDbParameterCollection dbParameterCollection,
        DataTable dataTable)
    {
        _dataTable = dataTable;
        DbConnection = dbConnection;
        DbParameterCollection = dbParameterCollection;
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
    protected override DbParameterCollection DbParameterCollection { get; } = new TestDbParameterCollection();
    protected override DbTransaction? DbTransaction { get; set; }
    public override bool DesignTimeVisible { get; set; }

    protected override DbParameter CreateDbParameter()
    {
        return new SqlParameter();
    }

    protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
    {
        return new DataTableReader(_dataTable);
    }
}
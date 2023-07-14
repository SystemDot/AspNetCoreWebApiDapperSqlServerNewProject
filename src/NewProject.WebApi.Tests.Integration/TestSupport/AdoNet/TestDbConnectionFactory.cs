using System.Data;
using NewProject.WebApi.Infrastructure.Dapper;

namespace NewProject.WebApi.Tests.Integration.TestSupport.AdoNet;

public class TestDbConnectionFactory : IDbConnectionFactory
{
    public TestDbConnection? DbConnection;
    private readonly Dictionary<string, DataTable> _dataTables = new();

    public IReadOnlyDictionary<string, DataTable> DataTables => _dataTables;

    public Task<IDbConnection> CreateAsync()
    {
        return Task.FromResult((DbConnection ??= new TestDbConnection(() => DataTables)) as IDbConnection);
    }

    public void AddOrUpdateDataTable(string commandText, IEnumerable<string> columnNames, params object[] rowValues)
    {
        var dataTable = new DataTable();

        foreach (var column in columnNames)
            dataTable.Columns.Add(column);

        if (rowValues.Any())
            dataTable.Rows.Add(rowValues);

        _dataTables[commandText] = dataTable;
    }
}
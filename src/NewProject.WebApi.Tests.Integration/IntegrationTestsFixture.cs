using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NewProject.Infrastructure.Dapper.SqlServer;
using Newtonsoft.Json.Linq;

namespace NewProject.WebApi.Tests.Integration;

public class IntegrationTestsFixture : IAsyncLifetime
{
    public readonly WebApplicationFactory<Program> WebApplicationFactory;

    public IntegrationTestsFixture()
    {
        WebApplicationFactory = new CustomWebApplicationFactory();
        //Db = new SqlConnection("MyConnectionString");

        // ... initialize data in the test database ...
    }

    //public SqlConnection Db { get; private set; }
    public Task InitializeAsync()
    {
        //throw new NotImplementedException();
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        //throw new NotImplementedException();
        await WebApplicationFactory.DisposeAsync();
    }
}

public class TestDbConnectionFactory : IDbConnectionFactory
{
    public TestDbConnection DbConnection = new();

    public Task<IDbConnection> CreateAsync()
    {
        return Task.FromResult(DbConnection as IDbConnection);
    }
}

public class TestDbConnection : DbConnection, IDbConnection
{
    public TestDbCommand DbCommand;
    public TestDbParameterCollection DbParameterCollection;

    //public void Dispose()
    //{
    //}

    //public IDbTransaction BeginTransaction()
    //{
    //    throw new NotImplementedException();
    //}

    //public IDbTransaction BeginTransaction(IsolationLevel il)
    //{
    //    throw new NotImplementedException();
    //}

    //public void ChangeDatabase(string databaseName)
    //{
    //    throw new NotImplementedException();
    //}

    //public void Close()
    //{
    //    throw new NotImplementedException();
    //}

    //public IDbCommand CreateCommand()
    //{
    //    DbCommand = new(this);
    //    return DbCommand;
    //}

    //public void Open()
    //{
    //    throw new NotImplementedException();
    //}

    //public string ConnectionString { get; set; }
    //public int ConnectionTimeout { get; }
    //public string Database { get; }
    //public ConnectionState State { get; }
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
        DbCommand = new(this, DbParameterCollection);
        return DbCommand;
    }
}

public class TestDbCommand : DbCommand, IDbCommand
{
    public TestDbCommand(DbConnection dbConnection, TestDbParameterCollection dbParameterCollection)
    {
        DbConnection = dbConnection;
        DbParameterCollection = dbParameterCollection;
    }
    //public void Dispose()
    //{
        
    //}

    //public void Cancel()
    //{
    //}

    //public IDbDataParameter CreateParameter()
    //{
    //    return new SqlParameter();
    //}

    //public int ExecuteNonQuery()
    //{
    //    return 1;
    //}

    //public IDataReader ExecuteReader()
    //{
    //    return new DataTableReader(new DataTable());
    //}

    //public IDataReader ExecuteReader(CommandBehavior behavior)
    //{
    //    return new DataTableReader(new DataTable());
    //}

    //public object? ExecuteScalar()
    //{
    //    return new { };
    //}

    //public void Prepare()
    //{
    //}

    //public string CommandText { get; set; }
    //public int CommandTimeout { get; set; }
    //public CommandType CommandType { get; set; }
    //public IDbConnection? Connection { get; set; }
    //public IDataParameterCollection Parameters { get; } = new TestDataParameterCollection();
    //public IDbTransaction? Transaction { get; set; }
    //public UpdateRowSource UpdatedRowSource { get; set; }
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
        return new DataTableReader(new DataTable());
    }
}

public class TestDbParameterCollection : DbParameterCollection, IDataParameterCollection
{
    public List<DbParameter> DbParameters = new();

    public override int Add(object value)
    {
        DbParameters.Add(value as DbParameter);
        return 1;
    }

    public override void Clear()
    {
        DbParameters.Clear();
    }

    public override bool Contains(object value)
    {
        return DbParameters.Contains(value);
    }

    public override int IndexOf(object value)
    {
        return DbParameters.IndexOf(value as DbParameter);
    }

    public override void Insert(int index, object value)
    {
        DbParameters.Insert(index, value as DbParameter);
    }

    public override void Remove(object value)
    {
        DbParameters.Remove(value as DbParameter);
    }

    public override void RemoveAt(int index)
    {
        DbParameters.RemoveAt(index);
    }

    public override void RemoveAt(string parameterName)
    {
        var param = DbParameters.SingleOrDefault(p => p.ParameterName == parameterName);
        if (param != null)
            DbParameters.Remove(param);
    }

    protected override void SetParameter(int index, DbParameter value)
    {
        DbParameters[index] = value;
    }

    protected override void SetParameter(string parameterName, DbParameter value)
    {
        var param = DbParameters.SingleOrDefault(p => p.ParameterName == parameterName);
        if (param != null)
            DbParameters[DbParameters.IndexOf(param)] = value;
    }

    public override int Count { get; }
    public override object SyncRoot { get; }

    public override int IndexOf(string parameterName)
    {
        var param = DbParameters.SingleOrDefault(p => p.ParameterName == parameterName);
        if (param != null)
            DbParameters.IndexOf(param);

        return -1;
    }

    public override bool Contains(string value)
    {
        return DbParameters.Any(p => p.ParameterName == value);
    }

    public override void CopyTo(Array array, int index)
    {
        throw new NotImplementedException();
    }

    public override IEnumerator GetEnumerator()
    {
        return DbParameters.GetEnumerator();
    }

    protected override DbParameter GetParameter(int index)
    {
        return DbParameters.ElementAt(index);
    }

    protected override DbParameter GetParameter(string parameterName)
    {
        return DbParameters.FirstOrDefault(p => p.ParameterName == parameterName);
    }

    public override void AddRange(Array values)
    {
        throw new NotImplementedException();
    }
}

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(services =>
                services
                    .AddSingleton<IDbConnectionFactory, TestDbConnectionFactory>());
    }
}
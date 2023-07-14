using System.Collections;
using System.Data;
using System.Data.Common;

namespace NewProject.WebApi.Tests.Integration.TestSupport.FakeAdoNet;

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
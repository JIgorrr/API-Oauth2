using Microsoft.Data.SqlClient;
using System.Collections.Concurrent;
using System.Data;

namespace Infrastructure.Data.Context;

public class DbContext()
{
    public ConcurrentDictionary<Guid, IDbTransaction?> DbTransaction { get; private set; } = [];
    public ConcurrentDictionary<Guid, IDbConnection?> DbConnection { get; private set; } = [];

    public async Task OpenConnection(string connectionString, Guid identificationKey)
    {
        SqlConnection dbConnection = new(connectionString);
        await dbConnection.OpenAsync();
        DbConnection.TryAdd(identificationKey, dbConnection);
    }

    public void OpenTransaction(IDbConnection? dbConnection, Guid identificationKey)
    {
        DbTransaction.TryAdd(identificationKey, dbConnection?.BeginTransaction());
    }

    public IDbTransaction? GetTransaction(Guid identificationKey)
    {
        if (DbTransaction.TryGetValue(identificationKey, out IDbTransaction? dbTransaction))
            return dbTransaction;

        return null;
    }

    public IDbConnection? GetConnection(Guid identificationKey)
    {
        if (DbConnection.TryGetValue(identificationKey, out IDbConnection? dbConnection))
            return dbConnection;

        return null;
    }
}

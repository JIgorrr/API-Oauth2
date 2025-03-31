using Application.Configurations;
using Application.Interfaces.Configurations;
using System.Collections.Concurrent;

namespace Infrastructure.Data.Context;

public class DbConnectionStringManager(ConcurrentDictionary<string, ConnectionStringOptions> connectionStrings) : IDbConnectionStringManager
{
    private readonly ConcurrentDictionary<string, ConnectionStringOptions> _connectionStrings = connectionStrings;

    public IDbConnectionSettings GetConnectionString(string keyValue)
    {
        if (_connectionStrings.TryGetValue(keyValue, out var connectionStringOptions))
            return connectionStringOptions;

        throw new Exception("ConnectionString não encontrada");
    }
}

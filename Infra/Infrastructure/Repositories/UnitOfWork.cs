using Application.Interfaces.Configurations;
using Application.Interfaces.Repositories;
using Infrastructure.Data.Context;
using System.Data;

namespace Infrastructure.Data.Repositories;

public class UnitOfWork(DbContext _dbContext, IDbConnectionStringManager dbConnectionStringManager) : IUnitOfWork
{
    #region Settings
    private readonly ThreadLocal<Guid> _identificationKey = new(() => Guid.NewGuid());
    private readonly IDbConnectionStringManager _dbConnectionStringManager = dbConnectionStringManager;
    private readonly DbContext _dbContext = _dbContext;
    private readonly object _locker = new();
    #endregion

    #region Repositories
    private ICredentialsRepository? _credentialsRepository;
    private ICertificateManagementRepository? _certificateManagementRepository;
    #endregion

    public ICredentialsRepository CredentialsRepository
    {
        get
        {
            InitConnection("Credentials", _identificationKey.Value).Wait();

            return _credentialsRepository ??= new CredentialsRepository(_dbContext.GetConnection(_identificationKey.Value), _dbContext.GetTransaction(_identificationKey.Value));
        }
    }

    public ICertificateManagementRepository CertificateManagementRepository
    {
        get
        {
            InitConnection("Credentials", _identificationKey.Value).Wait();

            return _certificateManagementRepository ??= new CertificateManagementRepository(_dbContext.GetConnection(_identificationKey.Value), _dbContext.GetTransaction(_identificationKey.Value));
        }
    }

    public void Commit()
    {
        lock (_locker)
        {
            foreach (KeyValuePair<Guid, IDbTransaction?> values in _dbContext.DbTransaction)
            {
                if (_dbContext.DbTransaction.TryGetValue(values.Key, out IDbTransaction? transaction))
                {
                    transaction?.Commit();
                    Dispose(values.Key);
                    _dbContext.DbTransaction.TryRemove(values.Key, out _);
                }
            }
        }
    }

    public void Rollback()
    {
        lock (_locker)
        {
            foreach (KeyValuePair<Guid, IDbTransaction?> values in _dbContext.DbTransaction)
            {
                if (_dbContext.DbTransaction.TryGetValue(values.Key, out IDbTransaction? transaction))
                {
                    transaction?.Rollback();
                    Dispose(values.Key);
                    _dbContext.DbTransaction.TryRemove(values.Key, out _);
                }
            }
        }
    }

    private async Task InitConnection(string database, Guid identificationKey)
    {
        IDbConnectionSettings dbConnectionSettings = _dbConnectionStringManager.GetConnectionString(database);

        await _dbContext.OpenConnection(dbConnectionSettings.ConnectionString, identificationKey);

        _dbContext.OpenTransaction(_dbContext?.GetConnection(identificationKey), identificationKey);
    }

    public void Dispose(Guid identificationKey)
    {
        _dbContext?.GetConnection(identificationKey)?.Dispose();
    }
}

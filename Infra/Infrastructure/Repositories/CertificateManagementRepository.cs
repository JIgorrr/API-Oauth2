using Application.Interfaces.Repositories;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Data.Repositories;

public class CertificateManagementRepository(IDbConnection? dbConnection, IDbTransaction? dbTransaction) : ICertificateManagementRepository
{
    public async Task<CertificateManagement?> GetById(long clieniId)
    {
        try
        {
            DynamicParameters parameters = new();
            parameters.Add("@clientId", clieniId);

            CertificateManagement? certificateManagement = await dbConnection!.QueryFirstOrDefaultAsync<CertificateManagement>("GetCertificateManagementByClientId", parameters, transaction: dbTransaction, commandType: CommandType.StoredProcedure);

            return certificateManagement;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

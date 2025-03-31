using Application.Interfaces.Repositories;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Data.Repositories;

public class CredentialsRepository(IDbConnection? dbConnection, IDbTransaction? dbTransaction) : ICredentialsRepository
{
    public async Task<ClientCredentials?> GetById(string clientId)
    {
        try
        {
            DynamicParameters parameters = new();
            parameters.Add("@clientId", clientId);

            ClientCredentials? clientCredentials = await dbConnection!.QueryFirstOrDefaultAsync<ClientCredentials>("GetClientCredentialsById", parameters, transaction: dbTransaction, commandType: CommandType.StoredProcedure);

            return clientCredentials;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

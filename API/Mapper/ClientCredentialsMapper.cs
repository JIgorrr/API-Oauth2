using Application.DTOs;
using GenerateAccessToken.Model;
using Mapster;
using System.Reflection;

namespace GenerateAccessToken.Mapper;

public static class ClientCredentialsMapper
{
    public static void RegisterClientCredentialsMapper(this IServiceCollection serviceDescriptors)
    {
        TypeAdapterConfig<ModelClientCredentials, ClientCredentialsDTO>
            .NewConfig()
            .Map(origin => origin.GrantType, destination => destination.GrantType)
            .Map(origin => origin.ClientId, destination => destination.ClientId)
            .Map(origin => origin.ClientSecret, destination => destination.ClientSecret)
            .Map(origin => origin.Scopes, destination => destination.Scopes);

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}

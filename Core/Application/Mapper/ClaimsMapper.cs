using Application.DTOs;
using Mapster;
using System.Reflection;

namespace Application.Mapper;

public static class ClaimsMapper
{
    public static void RegisterCliamsMapper()
    {
        TypeAdapterConfig<ClientCredentialsDTO, ClaimsInfoDTO>
            .NewConfig()
            .Map(destination => destination.Scopes, origin => origin.Scopes);

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}

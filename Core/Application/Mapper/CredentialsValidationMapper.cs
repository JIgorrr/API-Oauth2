using Application.DTOs;
using Domain.Entities;
using Mapster;
using System.Reflection;

namespace Application.Mapper;

public static class CredentialsValidationMapper
{
    public static void RegisterCredentialsValidationMapper()
    {
        TypeAdapterConfig<(ClientCredentialsDTO, ClientCredentials), CredentialsValidationDTO>
            .NewConfig()
            .MapWith(origin => new CredentialsValidationDTO()
            {
                PasswordRequest = origin.Item1.ClientSecret,
                StoredPassword = origin.Item2.ClientSecret,
                Salt = origin.Item2.Salt,
                RequestScopes = origin.Item1.Scopes,
                StoredScopes = origin.Item2.Scopes
            });

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}

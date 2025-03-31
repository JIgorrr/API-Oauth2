using Application.DTOs;
using Mapster;
using System.Reflection;

namespace Application.Mapper;

public static class CertificateInfoMapper
{
    public static void RegisterCertificateInfoMapper()
    {
        TypeAdapterConfig<(string, string?), CertificateInfoDTO>
            .NewConfig()
            .IgnoreNullValues(true)
            .Map(destination => destination.FilePath, origin => origin.Item1)
            .Map(destination => destination.Password, origin => origin.Item2 ?? null)
            .Compile();

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}

using Application.Clients.Validation;
using Application.Configurations;
using Application.Interfaces.Configurations;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.Validations;
using Application.Mapper;
using Application.Services;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Infrastructure.IoC.IoC;

public static class PipelineExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<ITokenService, TokenService>();
        service.AddScoped<ISHACryptographyService, SHACryptographyService>();
        service.AddScoped<ICertificateService, CertificateService>();
        service.AddScoped<IClientCredentialsValidation, ClientCredentialsValidation>();
        service.AddScoped<IAESCryptographyService, AESCryptographyService>();

        return service;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddTransient<DbContext>();
        service.AddTransient<IUnitOfWork, UnitOfWork>();

        service.AddTransient<IDbConnectionStringManager>(_ =>
        {
            var connectionStrings = new ConcurrentDictionary<string, ConnectionStringOptions>();

            configuration.GetSection(ConnectionStringOptions.ConnectionStrings).Bind(connectionStrings);

            return new DbConnectionStringManager(connectionStrings);
        });

        return service;
    }

    public static IServiceCollection AddConfigurationOptions(this IServiceCollection service, IConfiguration configuration)
    {
        return service;
    }

    public static IServiceCollection AddMapster(this IServiceCollection service)
    {
        CredentialsValidationMapper.RegisterCredentialsValidationMapper();
        ClaimsMapper.RegisterCliamsMapper();
        CertificateInfoMapper.RegisterCertificateInfoMapper();

        return service;
    }
}

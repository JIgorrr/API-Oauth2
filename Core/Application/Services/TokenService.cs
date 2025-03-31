using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.Validations;
using Domain.Entities;
using Jose;
using Mapster;

namespace Application.Services;

public class TokenService(IUnitOfWork unitOfWork, ICertificateService certificateService, IClientCredentialsValidation clientCredentialsValidation, IAESCryptographyService aesCryptographyService) : ITokenService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICertificateService _certificateService = certificateService;
    private readonly IClientCredentialsValidation _clientCredentialsValidation = clientCredentialsValidation;
    private readonly IAESCryptographyService _aesCryptographyService = aesCryptographyService;

    public async Task<TokenDetailsDTO> GenerateToken(ClientCredentialsDTO clientCredentialsDTO)
    {
        try
        {
            ClientCredentials clientCredentials = await _unitOfWork.CredentialsRepository.GetById(clientCredentialsDTO.ClientId)
                 ?? throw new("Credenciais não encontradas");

            _clientCredentialsValidation.ValidationHandler(ValueTuple.Create(clientCredentialsDTO, clientCredentials).Adapt<CredentialsValidationDTO>());

            CertificateManagement? certificateManagement = await _unitOfWork.CertificateManagementRepository.GetById(clientCredentials.Id);

            string? certificatePassword = null;

            if(!string.IsNullOrWhiteSpace(certificateManagement?.Password))
                certificatePassword = _aesCryptographyService.Decrypt(certificateManagement?.Password, certificateManagement?.Key, certificateManagement?.Vector);

            _certificateService.LoadCertificate(ValueTuple.Create(certificateManagement?.FilePath, certificatePassword).Adapt<CertificateInfoDTO>());

            Dictionary<string, string?> payload = CreateClaims(clientCredentialsDTO.Adapt<ClaimsInfoDTO>());

            string jwt = JWT.Encode(payload, _certificateService.GetPrivateKey, JwsAlgorithm.RS256);

            return new TokenDetailsDTO()
            {
                AccessToken = jwt,
                TokenType = "Bearer",
                ExpiresIn = payload.GetValueOrDefault("exp")
            };
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            _certificateService?.Dispose();
        }
    }

    private static Dictionary<string, string?> CreateClaims(ClaimsInfoDTO claimsInfoDTO)
    {
        string expirationTime = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds().ToString();

        return new Dictionary<string, string?>()
        {
                {"scopes",  claimsInfoDTO.Scopes },
                {"exp",  expirationTime },
                {"iat", DateTimeOffset.UtcNow.ToUniversalTime().ToUnixTimeSeconds().ToString() }
        };
    }
}

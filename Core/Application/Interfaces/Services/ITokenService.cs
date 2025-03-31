using Application.DTOs;

namespace Application.Interfaces.Services;

public interface ITokenService
{
    Task<TokenDetailsDTO> GenerateToken(ClientCredentialsDTO clientCredentials);
}

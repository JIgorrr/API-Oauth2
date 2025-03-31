using Application.Abstraction;
using Application.DTOs;
using Application.Interfaces.Services;

namespace Application.Handlers.ValidationHandler;

public class ValidationClientSecretHandler(ISHACryptographyService cryptographyService) : AbstractHandler
{
    private readonly ISHACryptographyService _cryptographyService = cryptographyService;

    public override object? Handle(object request)
    {
        CredentialsValidationDTO? credentialsValidationDTO = request as CredentialsValidationDTO
            ?? throw new Exception($"Falha na conversão da classe {nameof(CredentialsValidationDTO)}");

        bool isVerify = _cryptographyService.VerifyHash(credentialsValidationDTO.PasswordRequest!, credentialsValidationDTO.StoredPassword!, credentialsValidationDTO.Salt!);

        if (!isVerify)
            throw new("Credenciais inválidas");

        return base.Handle(request);
    }
}

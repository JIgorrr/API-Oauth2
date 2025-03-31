using Application.DTOs;
using Application.Handlers.ValidationHandler;
using Application.Interfaces.Services;
using Application.Interfaces.Validations;

namespace Application.Clients.Validation;

public class ClientCredentialsValidation(ISHACryptographyService cryptographyService) : IClientCredentialsValidation
{
    private readonly ISHACryptographyService _cryptographyService = cryptographyService;
    private readonly ValidationScopesHandler validationScopesHandler = new();

    public void ValidationHandler(CredentialsValidationDTO credentialsValidationDTO)
    {
        ValidationClientSecretHandler validationClientSecretHandler = new(_cryptographyService);

        validationClientSecretHandler.SetNext(validationScopesHandler);

        validationClientSecretHandler.Handle(credentialsValidationDTO);
    }
}

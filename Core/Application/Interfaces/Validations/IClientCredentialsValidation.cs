using Application.DTOs;

namespace Application.Interfaces.Validations;

public interface IClientCredentialsValidation
{
    void ValidationHandler(CredentialsValidationDTO credentialsValidationDTO);
}

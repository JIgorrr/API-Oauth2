using FluentValidation;
using GenerateAccessToken.Model;

namespace GenerateAccessToken.Validate;

public class ModelClientCredentialsValidate : AbstractValidator<ModelClientCredentials>
{
    public ModelClientCredentialsValidate()
    {
        RuleFor(x => x.ClientId)
            .NotNull()
            .WithMessage("Campo obrigatório");

        RuleFor(x => x.ClientSecret)
            .NotNull()
            .WithMessage("Campo obrigatório");

        RuleFor(x => x.GrantType)
            .NotNull()
            .WithMessage("Campo obrigatório");

        RuleFor(x => x.Scopes)
            .NotNull()
            .WithMessage("Campo obrigatório");
    }
}

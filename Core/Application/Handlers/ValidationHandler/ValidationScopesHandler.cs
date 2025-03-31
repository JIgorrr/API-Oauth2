using Application.Abstraction;
using Application.DTOs;

namespace Application.Handlers.ValidationHandler;

public class ValidationScopesHandler : AbstractHandler
{
    public override object? Handle(object request)
    {
        CredentialsValidationDTO? credentialsValidationDTO = request as CredentialsValidationDTO 
            ?? throw new Exception($"Falha na conversão da classe {nameof(CredentialsValidationDTO)}");

        string[] requestScopes = credentialsValidationDTO!.RequestScopes!.Split(',');

        string[] storedScopes = credentialsValidationDTO!.StoredScopes!.Split(',');

        List<string> invalidScopes = [];

        foreach(string scope in requestScopes)
        {
            if(!storedScopes.Contains(scope))
                invalidScopes.Add(scope);
        }

        if (invalidScopes.Count > 0)
            throw new Exception("O Cliente não possui acesso ao(s) scopo(s) ínformado(s)");

        return base.Handle(request);
    }
}

namespace Application.DTOs;

public class CredentialsValidationDTO
{
    public string? PasswordRequest { get; set; }

    public string? StoredPassword { get; set; }

    public int Salt { get; set; }

    public string? RequestScopes { get; set; } 

    public string? StoredScopes { get; set; } 
}

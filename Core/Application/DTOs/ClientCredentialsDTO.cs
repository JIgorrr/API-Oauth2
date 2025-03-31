namespace Application.DTOs;

public class ClientCredentialsDTO
{
    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }

    public string? GrantType { get; set; }

    public string? Scopes { get; set; }
}

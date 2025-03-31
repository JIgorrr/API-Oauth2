using Domain.Entities.Base;

namespace Domain.Entities;

public class ClientCredentials : BaseEntity
{
    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }

    public string? GrantType { get; set; }

    public string? ClientName { get; set; }
    
    public int Salt { get; set; }

    public string? Scopes { get; set; }
}

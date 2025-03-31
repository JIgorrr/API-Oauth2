using Domain.Entities.Base;

namespace Domain.Entities;

public class CertificateManagement : BaseEntity
{
    public string? FilePath { get; set; }

    public string? Password { get; set; }

    public string? Key { get; set; }

    public string? Vector { get; set; }

    public long? ClientCredentialsId { get; set; }
}

using Application.Interfaces.Configurations;

namespace Application.Configurations;

public class ConnectionStringOptions : IDbConnectionSettings
{
    public const string ConnectionStrings = nameof(ConnectionStringOptions);

    public string ConnectionString { get; set; } = string.Empty;

    public string ProviderName { get; set; } = string.Empty;
}

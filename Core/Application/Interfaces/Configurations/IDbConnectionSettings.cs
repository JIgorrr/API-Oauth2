namespace Application.Interfaces.Configurations;

public interface IDbConnectionSettings
{
    public string ConnectionString { get; set; }

    public string ProviderName { get; set; }
}
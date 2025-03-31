namespace Application.Interfaces.Configurations;

public interface IDbConnectionStringManager
{
    IDbConnectionSettings GetConnectionString(string keyValue);
}

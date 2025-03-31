namespace Application.Interfaces.Services;

public interface IAESCryptographyService
{
    string Decrypt(string? password, string? key, string? vector);
}

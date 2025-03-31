namespace Application.Interfaces.Services;

public interface ISHACryptographyService
{
    string GenerateHash(string text, int salt);

    bool VerifyHash(string requestPassword, string storedPassword, int salt);
}

using Application.DTOs;
using Application.Interfaces.Services;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Application.Services;

public class CertificateService : ICertificateService
{
    private X509Certificate2? _certificate;

    public void LoadCertificate(CertificateInfoDTO certificateInfoDTO)
    {
        if (!File.Exists(certificateInfoDTO.FilePath))
            throw new Exception("Caminho do certificado não encontrado");

        try
        {
            _certificate = new X509Certificate2(certificateInfoDTO.FilePath, certificateInfoDTO.Password);
        }
        catch (Exception)
        {
            throw new Exception("Não foi possível carregar o certificado");
        }
    }

    public RSA? GetPrivateKey
    {
        get => _certificate?.GetRSAPrivateKey();
    }

    public RSA? GetPublicKey
    {
        get => _certificate?.GetRSAPublicKey();
    }

    public void Dispose()
    {
        _certificate?.Dispose();
    }
}

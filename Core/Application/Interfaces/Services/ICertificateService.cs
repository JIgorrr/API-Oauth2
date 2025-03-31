using Application.DTOs;
using System.Security.Cryptography;

namespace Application.Interfaces.Services;

public interface ICertificateService
{
    public RSA? GetPrivateKey { get; }

    public RSA? GetPublicKey { get; }

    void LoadCertificate(CertificateInfoDTO certificateInfoDTO);

    void Dispose();
}

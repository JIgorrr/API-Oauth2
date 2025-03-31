using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICertificateManagementRepository
{
    Task<CertificateManagement?> GetById(long clientId);
}

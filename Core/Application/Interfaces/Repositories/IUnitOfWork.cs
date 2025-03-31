namespace Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    public ICredentialsRepository CredentialsRepository { get; }

    public ICertificateManagementRepository CertificateManagementRepository { get; }

    void Commit();

    void Rollback();
}

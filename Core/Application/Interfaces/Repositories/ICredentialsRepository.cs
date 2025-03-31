using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICredentialsRepository
{
    Task<ClientCredentials?> GetById(string? clientId);
}

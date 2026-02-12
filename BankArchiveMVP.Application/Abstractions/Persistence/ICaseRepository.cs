using BankArchiveMVP.Domain.Entities;

namespace BankArchiveMVP.Application.Abstractions.Persistence;

public interface ICaseRepository
{
    Task<Case?> GetByCaseNoAsync(string caseNo, CancellationToken ct = default);
    Task AddAsync(Case @case, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}

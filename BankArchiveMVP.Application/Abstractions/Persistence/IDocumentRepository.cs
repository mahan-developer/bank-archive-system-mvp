using BankArchiveMVP.Domain.Entities;

namespace BankArchiveMVP.Application.Abstractions.Persistence;

public interface IDocumentRepository
{
    Task AddAsync(Document document, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
    Task<bool> ExistsByCaseAndHashAsync(Guid caseId, string fileHash, CancellationToken ct = default);

}

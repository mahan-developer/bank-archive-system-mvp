using BankArchiveMVP.Application.Abstractions.Persistence;
using BankArchiveMVP.Domain.Entities;
using BankArchiveMVP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankArchiveMVP.Infrastructure.Persistence.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _context;

        public DocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Document document, CancellationToken ct = default)
        {
            await _context.Documents.AddAsync(document, ct);
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> ExistsByCaseAndHashAsync(Guid caseId, string fileHash, CancellationToken ct = default)
        {
            return await _context.Documents
                .AnyAsync(d => d.CaseId == caseId && d.FileHash == fileHash, ct);
        }
    }
}

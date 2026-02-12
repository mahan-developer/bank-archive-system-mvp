using BankArchiveMVP.Application.Abstractions.Persistence;
using BankArchiveMVP.Domain.Entities;
using BankArchiveMVP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankArchiveMVP.Infrastructure.Persistence.Repositories;

public class CaseRepository : ICaseRepository
{
    private readonly AppDbContext _context;

    public CaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Case?> GetByCaseNoAsync(string caseNo, CancellationToken ct = default)
    {
        return await _context.Cases
            .FirstOrDefaultAsync(c => c.CaseNo == caseNo, ct);
    }

    public async Task AddAsync(Case @case, CancellationToken ct = default)
    {
        await _context.Cases.AddAsync(@case, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }
}

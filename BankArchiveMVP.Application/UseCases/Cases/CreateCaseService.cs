using BankArchiveMVP.Application.Abstractions.Persistence;
using BankArchiveMVP.Domain.Entities;
using BankArchiveMVP.Domain.Enums;

namespace BankArchiveMVP.Application.UseCases.Cases;

public class CreateCaseService
{
    private readonly ICaseRepository _caseRepo;
    private readonly ICustomerRepository _customerRepo;

    public CreateCaseService(
        ICaseRepository caseRepo,
        ICustomerRepository customerRepo)
    {
        _caseRepo = caseRepo;
        _customerRepo = customerRepo;
    }

    public async Task<Guid> CreateAsync(
        string caseNo,
        string customerNo,
        CaseType caseType,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(caseNo))
            throw new InvalidOperationException("CaseNo is required.");

        if (string.IsNullOrWhiteSpace(customerNo))
            throw new InvalidOperationException("CustomerNo is required.");

        caseNo = caseNo.Trim();
        customerNo = customerNo.Trim();

        var existingCase = await _caseRepo.GetByCaseNoAsync(caseNo, ct);
        if (existingCase != null)
            throw new InvalidOperationException("CaseNo already exists.");

        var customer = await _customerRepo.GetByCustomerNoAsync(customerNo, ct);
        if (customer == null)
            throw new InvalidOperationException("Customer not found.");

        var @case = new Case
        {
            Id = Guid.NewGuid(),
            CaseNo = caseNo,
            CustomerId = customer.Id,
            CaseType = caseType,
            CaseStatus = CaseStatus.Open,
            CreatedAt = DateTime.UtcNow
        };

        await _caseRepo.AddAsync(@case, ct);
        await _caseRepo.SaveChangesAsync(ct);

        return @case.Id;
    }
}

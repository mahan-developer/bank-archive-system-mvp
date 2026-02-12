using BankArchiveMVP.Application.Abstractions.Persistence;
using BankArchiveMVP.Domain.Entities;

namespace BankArchiveMVP.Application.UseCases.Customers;

public class CreateCustomerService
{
    private readonly ICustomerRepository _repo;

    public CreateCustomerService(ICustomerRepository repo)
    {
        _repo = repo;
    }

    public async Task<Guid> CreateAsync(
        string customerNo,
        string firstName,
        string lastName,
        string? nationalId,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(customerNo))
            throw new InvalidOperationException("CustomerNo is required.");

        if (string.IsNullOrWhiteSpace(firstName))
            throw new InvalidOperationException("FirstName is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new InvalidOperationException("LastName is required.");


        var existing = await _repo.GetByCustomerNoAsync(customerNo, ct);
        if (existing != null)
            throw new InvalidOperationException("CustomerNo already exists.");

        customerNo = customerNo.Trim();
        firstName = firstName.Trim();
        lastName = lastName.Trim();
        nationalId = string.IsNullOrWhiteSpace(nationalId) ? null : nationalId.Trim();

        // MVP validation for NationalId (if provided)
        if (nationalId != null)
        {
            // فقط عدد و دقیقاً 10 رقم (اگر DB همینو می‌خواد)
            if (nationalId.Length != 10 || !nationalId.All(char.IsDigit))
                throw new InvalidOperationException("NationalId must be 10 digits (numbers only).");
        }


        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            CustomerNo = customerNo,
            FirstName = firstName,
            LastName = lastName,
            NationalId = nationalId,
            Status = "Active",
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(customer, ct);

        await _repo.SaveChangesAsync(ct);


        return customer.Id;
    }
}

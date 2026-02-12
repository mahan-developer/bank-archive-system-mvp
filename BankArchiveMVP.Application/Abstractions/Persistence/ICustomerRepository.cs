using BankArchiveMVP.Domain.Entities;

namespace BankArchiveMVP.Application.Abstractions.Persistence;

public interface ICustomerRepository
{
    Task<Customer?> GetByCustomerNoAsync(string customerNo, CancellationToken ct = default);
    Task AddAsync(Customer customer, CancellationToken ct = default);
    
    Task SaveChangesAsync(CancellationToken ct = default);

}

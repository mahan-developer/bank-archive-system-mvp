using BankArchiveMVP.Application.Abstractions.Persistence;
using BankArchiveMVP.Infrastructure.Persistence;
using BankArchiveMVP.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankArchiveMVP.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("Default")));

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICaseRepository, CaseRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();

        return services;
    }
}

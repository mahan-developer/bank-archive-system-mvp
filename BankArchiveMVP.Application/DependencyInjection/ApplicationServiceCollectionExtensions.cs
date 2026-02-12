using BankArchiveMVP.Application.UseCases.Cases;
using BankArchiveMVP.Application.UseCases.Customers;
using BankArchiveMVP.Application.UseCases.Documents;
using Microsoft.Extensions.DependencyInjection;

namespace BankArchiveMVP.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<CreateCustomerService>();
            services.AddTransient<CreateCaseService>();
            services.AddTransient<CreateDocumentService>();


            return services;
        }
    }
}

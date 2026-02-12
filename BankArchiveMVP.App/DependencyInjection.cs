using Microsoft.Extensions.DependencyInjection;

namespace BankArchiveMVP.App;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddTransient<MainWindow>();
        services.AddTransient<MainWindowViewModel>();

        return services;
    }
}

namespace PaymentsSharing.Refunds;

public static class RefundsExtensionMethods
{
    public static IServiceCollection AddRefunds(this IServiceCollection services)
    {
        services.AddSingleton<Refunds>();
        services.AddTransient<RefundsTableViewModel>();
        return services;
    }
}
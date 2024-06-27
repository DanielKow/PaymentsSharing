namespace PaymentsSharing.Payments;

internal static class PaymentsExtensionMethods
{
    public static IServiceCollection AddPayments(this IServiceCollection services)
    {
        services.AddSingleton<Payments>();
        services.AddTransient<AddPaymentFormViewModel>();
        services.AddTransient<AddPaymentFromDateFormViewModel>();
        services.AddTransient<PaymentsListViewModel>();
        
        return services;
    }
}
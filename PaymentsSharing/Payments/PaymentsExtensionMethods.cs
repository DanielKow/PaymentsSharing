namespace PaymentsSharing.Payments;

internal static class PaymentsExtensionMethods
{
    public static IServiceCollection AddPayments(this IServiceCollection services)
    {
        services.AddSingleton<Payments>();
        services.AddTransient<AddPaymentFormViewModel>();
        return services;
    }
}
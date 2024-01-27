namespace PaymentsSharing.Summary;

internal static class SummaryExtensionsMethods
{
    public static IServiceCollection AddSummary(this IServiceCollection services)
    {
        services.AddTransient<SummaryViewModel>();
        return services;
    }
}
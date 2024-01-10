namespace PaymentsSharing.Users;

internal static class UsersExtensionMethods
{
    public static IServiceCollection AddUsers(this IServiceCollection services)
    {
        services.AddSingleton<ExistingUsers>();
        return services;
    }
}
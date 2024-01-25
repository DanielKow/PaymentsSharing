namespace PaymentsSharing.SignIn;

internal static class SignInExtensionMethods
{
    public static IServiceCollection AddSignIn(this IServiceCollection services)
    {
        services.AddSingleton<Users>();
        services.AddTransient<SignInFormViewModel>();

        return services;
    }
}
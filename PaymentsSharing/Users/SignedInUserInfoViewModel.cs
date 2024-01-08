using Microsoft.AspNetCore.Components;

namespace PaymentsSharing.Users;

internal class SignedInUserInfoViewModel(NavigationManager navigationManager)
{
    public void SignIn()
    {
        navigationManager.NavigateTo("/SignInForm");
    }
    
    public void SignOut()
    {
        navigationManager.NavigateTo("/");
    }
}
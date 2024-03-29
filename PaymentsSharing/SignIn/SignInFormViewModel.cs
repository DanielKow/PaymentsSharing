using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PaymentsSharing.Persons;

namespace PaymentsSharing.SignIn;

internal class SignInFormViewModel(Users users, IPublisher mediator, NavigationManager navigationManager)
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public bool WrongCredentials { get; private set; }
    
    public void OnKeyDown(KeyboardEventArgs eventArgs)
    {
        if (eventArgs.Key == "Enter")
        {
            SignIn();
        }
    }
    
    public void SignIn()
    {
        if (!users.CheckCredentials(new User(Username, Password)))
        {
            WrongCredentials = true;
            return;
        }

        WrongCredentials = false;
        mediator.Publish(new PersonSignedIn(Username));
        navigationManager.NavigateTo("/");
    }
}
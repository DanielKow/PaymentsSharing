using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PaymentsSharing.Users;
using TechTalk.SpecFlow;

namespace PaymentsSharing.Specs.Steps;

[Binding]
public class UserSteps
{
    private readonly ExistingUsers _existingUsers;
    private readonly IMediator _mediator;
    private string _username = string.Empty;
    private string _password = string.Empty;
    private bool _isMeatEater;

    public UserSteps()
    {
        _existingUsers = new ExistingUsers();
        
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));
        serviceCollection.AddSingleton(_existingUsers);
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _mediator = serviceProvider.GetRequiredService<IMediator>();
    }
    
    [Given("""
           username is "(.*)"
           """)]
    public void GivenUsernameIs(string username)
    {
        _username = username;
    }

    [Given("""
           password is "(.*)"
           """)]
    public void GivenPasswordIs(string password)
    {
        _password = password;
    }
    
    [Given("user does not eat meat")]
    public void GivenUserDoesNotEatMeat()
    {
        _isMeatEater = false;
    }

    [When("user is created")]
    public async Task WhenUserIsCreated()
    {
        var createUser = new CreateUser(_username, _password, _isMeatEater);
        await _mediator.Send(createUser);
    }

    [Then("""user "(.*)" should exist""")]
    public void ThenUserShouldExist(string username)
    {
        _existingUsers.FirstOrDefault(user => user.Username == username)?.Should().NotBeNull();
    }
}
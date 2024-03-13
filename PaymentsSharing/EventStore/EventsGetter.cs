using PaymentsSharing.Payments;
using PaymentsSharing.SignIn;

namespace PaymentsSharing.EventStore;

internal class EventsGetter(IEventStore eventStore, Payments.Payments payments, Users users)
{
    public async Task GetEvents()
    {
        await GetUsers();
        await GetPayments();
    }

    private async Task GetUsers()
    {
        Console.WriteLine("Getting users");
        
        IEnumerable<UserAdded?> events = await eventStore.GetEvents<UserAdded>();

        foreach (UserAdded? userAdded in events)
        {
            if (userAdded is null)
            {
                continue;
            }
            
            users.AddOrUpdate(new User(userAdded.Username, userAdded.Password));
        }

        Console.WriteLine("Got users");
    }

    private async Task GetPayments()
    {
        Console.WriteLine("Getting payments");

        IEnumerable<PaymentAdded?> events = await eventStore.GetEvents<PaymentAdded>();

        foreach (PaymentAdded? @event in events)
        {
            if (@event is null)
            {
                continue;
            }
    
            payments.Add(new Payment(
                @event.CreatedAt,
                @event.Payers,
                @event.Consumers,
                @event.Amount,
                @event.AmountForMeat,
                @event.Description));
        }
        
        Console.WriteLine("Got payments");
    }
}
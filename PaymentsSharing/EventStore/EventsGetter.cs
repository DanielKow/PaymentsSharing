using PaymentsSharing.Payments;

namespace PaymentsSharing.EventStore;

internal class EventsGetter(IEventStore eventStore, Payments.Payments payments)
{
    public async Task GetEvents()
    {
        await GetPayments();
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
                @event.Date,
                @event.Amount,
                @event.AmountForMeat,
                @event.Description));
        }
        
        Console.WriteLine("Got payments");
    }
}
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

        IEnumerable<PaymentAdded?> added = await eventStore.GetEvents<PaymentAdded>();

        foreach (PaymentAdded? @event in added)
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
        
        IEnumerable<PaymentRemoved?> removed = await eventStore.GetEvents<PaymentRemoved>();
        
        foreach (PaymentRemoved? @event in removed)
        {
            if (@event is null)
            {
                continue;
            }
    
            payments.Remove(@event.Payment);
        }
        
        Console.WriteLine("Got payments");
    }
}
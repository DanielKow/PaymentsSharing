using Microsoft.EntityFrameworkCore;

namespace PaymentsSharing.EventStore;

internal class EventsContext : DbContext
{
    public DbSet<Event> Events { get; set; } = null!;
    
    public EventsContext(DbContextOptions<EventsContext> options) : base(options)
    {
        
    }
}
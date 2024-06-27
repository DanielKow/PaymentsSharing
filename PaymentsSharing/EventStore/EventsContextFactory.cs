using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PaymentsSharing.EventStore;

internal class EventsContextFactory : IDesignTimeDbContextFactory<EventsContext>
{
    public EventsContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EventsContext>();
        optionsBuilder.UseNpgsql("User ID=root;Password=root;Host=localhost;Port=5432;Database=payments_sharing;");
        return new EventsContext(optionsBuilder.Options);
    }
}
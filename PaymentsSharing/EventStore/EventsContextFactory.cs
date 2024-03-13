using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PaymentsSharing.EventStore;

internal class EventsContextFactory : IDesignTimeDbContextFactory<EventsContext>
{
    public EventsContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EventsContext>();
        optionsBuilder.UseSqlServer("Server=tcp:andrew-server.database.windows.net,1433;Initial Catalog=payments_sharing;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;");
        return new EventsContext(optionsBuilder.Options);
    }
}
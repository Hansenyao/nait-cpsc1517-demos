using Microsoft.EntityFrameworkCore;

namespace ExampleOOPWebApp.Components.Models
{
    public class EventsContext : DbContext
    {
        public EventsContext(DbContextOptions<EventsContext> options)
            : base(options)
        { 
        }

        public DbSet<EventsItem> EventsItems { get; set; }
    }
}

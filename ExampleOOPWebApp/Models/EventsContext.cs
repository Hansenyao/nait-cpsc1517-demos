using Microsoft.EntityFrameworkCore;

namespace ExampleOOPWebApp.Models
{
    public class EventsContext : DbContext
    {
        private static bool _initialized = false;
        public EventsContext(DbContextOptions<EventsContext> options)
            : base(options)
        {
            if (!_initialized)
            {
                EventsSeedData.Initialize(this);
                _initialized = true;
            }
        }

        public DbSet<EventsItem> EventsItems { get; set; }
    }
}

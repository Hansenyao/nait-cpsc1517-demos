using ExampleOOPWebApp.Models;
using ExampleOOPWebApp.Service.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ExampleOOPWebApp.Service
{
    public class EventsItemService : IEventsItemService
    {
        private readonly EventsContext _eventsContext;

        public EventsItemService(EventsContext eventsContext)
        {
            _eventsContext = eventsContext;
        }

        public async Task<IList<EventsItem>> GetEventItemsAsync()
        {
            return await _eventsContext.EventsItems.ToListAsync();
        }

        public async Task<EventsItem> GetEventItemAsync(string id)
        {
            return await _eventsContext.EventsItems.FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<EventsItem> AddEventItemAsync(EventsItem item)
        {
            item.Id = Guid.NewGuid().ToString();
            item.IsComplete = false;

            _eventsContext.EventsItems.Add(item);
            await _eventsContext.SaveChangesAsync();

            return item;
        }
        public async Task<EventsItem> UpdateEventItemAsync(EventsItem item)
        {
            _eventsContext.EventsItems.Update(item);
            await _eventsContext.SaveChangesAsync();

            return item;
        }
        public async Task<EventsItem> DeleteEventItemAsync(EventsItem item)
        {
            _eventsContext.EventsItems.Remove(item);
            await _eventsContext.SaveChangesAsync();

            return item;
        }
    }
}

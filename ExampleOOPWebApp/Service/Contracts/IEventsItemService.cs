using ExampleOOPWebApp.Models;

namespace ExampleOOPWebApp.Service.Contracts
{
    public interface IEventsItemService
    {
        Task<IList<EventsItem>> GetEventItemsAsync();
        Task<EventsItem> GetEventItemAsync(string id);
        Task<EventsItem> AddEventItemAsync(EventsItem item);
        Task<EventsItem> UpdateEventItemAsync(EventsItem item);
        Task<EventsItem> DeleteEventItemAsync(EventsItem item);
    }
}

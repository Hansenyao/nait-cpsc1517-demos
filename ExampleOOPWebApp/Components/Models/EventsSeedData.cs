namespace ExampleOOPWebApp.Components.Models
{
    public class EventsSeedData
    {
        public static void Initialize(EventsContext db)
        {
            var items = new EventsItem[]
            {
                new EventsItem { Id = Guid.NewGuid().ToString(), Name = "Study Computer Network", IsComplete=false, Remark = "Take a Test" },
                new EventsItem { Id = Guid.NewGuid().ToString(), Name = "Study Operation System", IsComplete=false, Remark = "Take a Test" },
                new EventsItem { Id = Guid.NewGuid().ToString(), Name = "Study Data Structure", IsComplete=false, Remark = "Take a Test" },
                new EventsItem { Id = Guid.NewGuid().ToString(), Name = "Walk the dog", IsComplete=true, Remark = string.Empty },
                new EventsItem { Id = Guid.NewGuid().ToString(), Name = "Run 5km in 40mins", IsComplete=true, Remark = string.Empty },
            };
            db.EventsItems.AddRange(items);
            db.SaveChanges();
        }
    }
}

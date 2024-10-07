using ExampleOOPWebApp.Components;
using ExampleOOPWebApp.Models;
using ExampleOOPWebApp.Service;
using ExampleOOPWebApp.Service.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add database context
builder.Services.AddDbContext<EventsContext>(opt => 
    opt.UseInMemoryDatabase("DailyEventsList"));
//builder.Services.AddSingleton<IEventsItemService, EventsItemService>();   //Failed
builder.Services.AddScoped<IEventsItemService, EventsItemService>();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

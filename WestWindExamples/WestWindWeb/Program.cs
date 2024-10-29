using Microsoft.EntityFrameworkCore;
using WestWindLibrary.BLL;
using WestWindLibrary.DAL;
using WestWindWeb.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Injecting servixes must comes before the builder is built!
builder.Services.AddDbContext<WestWindContext>(options => options.UseSqlServer
    (builder.Configuration.GetConnectionString("WWDB")));

builder.Services.AddScoped<ProductServices, ProductServices>();

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

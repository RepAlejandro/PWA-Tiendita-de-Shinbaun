using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Proyectotiendita.Services;
using Proyectotiendita;
using TiendaS.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<Geolocalizacion>();
builder.Services.AddScoped<IUsersService, UserService>();

// Registro de Blazored.LocalStorage
builder.Services.AddBlazoredLocalStorage();


builder.Services.AddHttpClient<IProductService, ProductService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44375");
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44306/") });
builder.Services.AddScoped<IUsersService, UserService>();


await builder.Build().RunAsync();

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MiTienda.Web;
using MiTienda.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Direccion de la Api (MiTienda.Api): se lee de wwwroot/appsettings.json
// (en Development usa appsettings.Development.json, que apunta a localhost)
var apiUrl = builder.Configuration["ApiUrl"] ?? "https://localhost:7101/";
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiUrl)
});

builder.Services.AddScoped<ClientesApi>();

await builder.Build().RunAsync();

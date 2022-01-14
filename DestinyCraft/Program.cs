using BungieSharper.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DestinyCraft;
using DestinyCraft.ViewModels;
using DotNetBungieAPI;
using DotNetBungieAPI.Clients;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddScoped<IIndexViewModel, IndexViewModel>();

// builder.Services.AddScoped(sp => new HttpClient
// {
//     BaseAddress = new Uri("http://localhost:5000/api/")
// });
//
// BungieClientConfig config = new BungieClientConfig();
// config = new BungieClientConfig();
// config.ApiKey = "02920a8d5c614cc992a23c348e07bf9e";
// config.OAuthClientId = 38045;
//
// builder.Services.AddScoped(sp => new BungieApiClient(config));

await builder.Build().RunAsync();
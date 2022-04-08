using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SaveAnAnimal.ApiClient;
using SaveAnAnimal.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string BaseApiUrl = "http://localhost:5236";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<SaveAnAnimalClient>(sp => new SaveAnAnimalClient(BaseApiUrl));
builder.Services.AddLogging();

await builder.Build().RunAsync();

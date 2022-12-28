using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PayDec.Client;
using PayDec.Client.Services.Repository.Interfaces;
using PayDec.Client.Services.Repository;
using Blazored.LocalStorage;
using System.Text.Json;
using System.Text.Json.Serialization;
using PayDec.Client.Services.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using MetaMask.Blazor;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSingleton(sp => new HttpClient { 
    BaseAddress = new Uri(builder.Configuration.GetSection("ApiSettings:WebApiUrl").Value) 
});

builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    config.JsonSerializerOptions.WriteIndented = false;
});

builder.Services.AddScoped<AuthenticationStateProvider,PDAuthenticationStateProvider>();

builder.Services.AddAuthorizationCore();

builder.Services.AddMetaMaskBlazor();


await builder.Build().RunAsync();

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Nethereum.Web3;
using PayDec.Client;
using PayDec.Client.Services.Repository.Interfaces;
using PayDec.Client.Services.Repository;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using System.Text.Json;
using System.Text.Json.Serialization;
using PayDec.Client.Services.Authentication;
using Microsoft.AspNetCore.Components.Authorization;

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

//builder.Services.AddScoped<IRepository, Authenticator>();

//await GetAccountBalance();
//Console.ReadLine(); 

await builder.Build().RunAsync();

//static async Task GetAccountBalance()
//{
//    var web3 = new Web3("https://mainnet.infura.io/v3/e126aa853d8248adb96b087e545275f3");
//    var balance = await web3.Eth.GetBalance.SendRequestAsync("0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae");
//    Console.WriteLine($"Balance in Wei: {balance.Value}");

//    var etherAmount = Web3.Convert.FromWei(balance.Value);
//    Console.WriteLine($"Balance in Ether: {etherAmount}");
//}
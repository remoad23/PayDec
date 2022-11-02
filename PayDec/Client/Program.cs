using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Nethereum.Web3;
using PayDec.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


//await GetAccountBalance();
//Console.ReadLine(); 


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

//static async Task GetAccountBalance()
//{
//    var web3 = new Web3("https://mainnet.infura.io/v3/e126aa853d8248adb96b087e545275f3");
//    var balance = await web3.Eth.GetBalance.SendRequestAsync("0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae");
//    Console.WriteLine($"Balance in Wei: {balance.Value}");

//    var etherAmount = Web3.Convert.FromWei(balance.Value);
//    Console.WriteLine($"Balance in Ether: {etherAmount}");
//}
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Nethereum.Contracts.Standards.ERC20.TokenList;
using Newtonsoft.Json.Linq;
using PayDec.Shared.Model.Authentication;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace PayDec.Client.Services.Authentication
{
    public class PDAuthenticationStateProvider : AuthenticationStateProvider
    {
            private HttpClient Client { get; set; }
            ILocalStorageService LocalStorage { get; set; }


            public PDAuthenticationStateProvider(HttpClient client,ILocalStorageService localStorage)
            {
                Client = client;
                LocalStorage = localStorage;
            }

            public override async Task<AuthenticationState> GetAuthenticationStateAsync()
            {
                var identity = new ClaimsIdentity();
                try
                {
                    var isAuthenticated = await Authenticate();
                    if(isAuthenticated.Item2 == "notoken")
                    {
                        var claim = new ClaimsPrincipal(new ClaimsIdentity(Array.Empty<Claim>(), string.Empty));
                        return new AuthenticationState(claim);
                    }
                    var isAnonymous = await AuthenticateAnonymous();
                    if (isAuthenticated.Item1)
                    {
                        Claim[] claims = new[] { new Claim(ClaimTypes.Authentication,"Authenticated") };
                        identity = new ClaimsIdentity(claims, "Server authentication");
                    }
                    else if(isAnonymous.Item1)
                    {
                        Claim[] claims = new[] { new Claim(ClaimTypes.Anonymous, "Anonymous") };
                        identity = new ClaimsIdentity(claims, "Server authentication");
                    }
                    else
                    {
                        var claim = new ClaimsPrincipal(new ClaimsIdentity(Array.Empty<Claim>(), string.Empty));
                        return new AuthenticationState(claim);
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine("Request failed:" + ex.ToString());
                }

                return new AuthenticationState(new ClaimsPrincipal(identity));
            }

        private async Task<(bool,string)> Authenticate()
        {
            string? token = await LocalStorage.GetItemAsync<string>("token");

            if (token != null && token != "")
            {
                token = token[0..(token.Length)];
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                return (false, "notoken");
            }


            bool result = await Client.GetFromJsonAsync<bool>("/Authenticate");
            return (result, "auth");
        }

        private async Task<(bool, string)> AuthenticateAnonymous()
        {
            string? token = await LocalStorage.GetItemAsync<string>("token");

            if (token != null && token != "")
            {
                token = token[0..(token.Length)];
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                return (false, "notoken");
            }

            bool result = await Client.GetFromJsonAsync<bool>("/AuthenticateAnonymous");
            return (result, "auth");
        }


        public async Task Logout()
            {
                await Client.GetAsync("/Logout");
                await LocalStorage.RemoveItemAsync("token");
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
                await LocalStorage.RemoveItemAsync("ItemsToBuy");
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }

            public async Task Login(LoginRequest login)
            {
                var result = await Client.PostAsJsonAsync<LoginRequest>("/Login",login);
                string token = await result.Content.ReadAsStringAsync();
                await LocalStorage.SetItemAsync("token", token);
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        
    }
}

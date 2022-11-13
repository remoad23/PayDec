using Microsoft.AspNetCore.Components.Authorization;
using PayDec.Shared.Model.Authentication;
using System.Net.Http.Json;
using System.Security.Claims;

namespace PayDec.Client.Services.Authentication
{
    public class PDAuthenticationStateProvider : AuthenticationStateProvider
    {
            private CurrentUser User { get; set; }
            private HttpClient Client { get; set; }
            public PDAuthenticationStateProvider(HttpClient client)
            {
                Client = client;
            }

            public override async Task<AuthenticationState> GetAuthenticationStateAsync()
            {
                var identity = new ClaimsIdentity();
                try
                {
                    var userInfo = await GetCurrentUser();
                    if (userInfo.IsAuthenticated)
                    {
                        var claims = new[] { new Claim(ClaimTypes.Name, User.UserName) }.Concat(User.Claims.Select(c => new Claim(c.Key, c.Value)));
                        identity = new ClaimsIdentity(claims, "Server authentication");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine("Request failed:" + ex.ToString());
                }
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }

            private async Task<CurrentUser> GetCurrentUser()
            {
                if (User != null && User.IsAuthenticated) return User;

                User = await Client.GetFromJsonAsync("/CurrentUser/");
                return User;
            }

            public async Task Logout()
            {
                await Client.GetFromJsonAsync("/Logout");
                User = null;
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }

            public async Task Login(LoginRequest login)
            {
                await Client.PostAsJsonAsync("/Logout",login);

                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        
    }
}

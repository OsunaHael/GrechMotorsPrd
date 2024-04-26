using GrechMotorsPrd.Client.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace GrechMotorsPrd.Client.Auth
{
    public class JwtAuthProvider : AuthenticationStateProvider, ILoginService
    {
        private readonly IJSRuntime js;
        private readonly HttpClient httpClient;
        public static readonly string TOKENKEY = "TOKENKEY"; 
        private AuthenticationState Anonimo => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new ClaimsIdentity())));

        public JwtAuthProvider(IJSRuntime js, HttpClient httpClient)
        {
            this.js = js;
            this.httpClient = httpClient;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetFromLocalStorage(TOKENKEY);
            if(token is null)
            {
                return Anonimo;
            }
            return BuildAuthenticationState(token.ToString()!);
        }

        private AuthenticationState BuildAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var claims = ParseJWTClaims(token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
        }

        private IEnumerable<Claim> ParseJWTClaims(string token)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var deserliazedToken = jwtSecurityTokenHandler.ReadJwtToken(token);
            return deserliazedToken.Claims;
        }

        public async Task Login(string token)
        {
            await js.SaveInLocalStorage(TOKENKEY, token);
            var authState = BuildAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await js.RemoveFromLocalStorage(TOKENKEY);
            httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
        }
    }
}
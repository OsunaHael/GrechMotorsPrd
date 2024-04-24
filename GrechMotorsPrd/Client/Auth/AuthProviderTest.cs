using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace GrechMotorsPrd.Client.Auth
{
    public class AuthProviderTest : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonimo = new ClaimsIdentity();
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonimo)));
        }
    }
}
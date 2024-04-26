using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace GrechMotorsPrd.Client.Auth
{
    public class AuthProviderTest : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonimo = new ClaimsIdentity();
            var usuarioPlanner = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Planner"),
                    new Claim("NumeroEmpleado", "4645"),
                    new Claim(ClaimTypes.Name, "Esthefania"),
                },
                authenticationType: "prueba");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(usuarioPlanner)));
        }
    }
}
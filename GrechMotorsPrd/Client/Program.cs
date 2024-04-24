using CurrieTechnologies.Razor.SweetAlert2;
using GrechMotorsPrd.Client;
using GrechMotorsPrd.Client.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using QRCoder;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
ConfigureServices(builder.Services);

// Registra QrCodeGenerator como un servicio
builder.Services.AddSingleton<QrCodeGenerator>();

await builder.Build().RunAsync();

void ConfigureServices(IServiceCollection services)
{
    services.AddSweetAlert2();
    services.AddAuthorizationCore();
    services.AddScoped<AuthenticationStateProvider, AuthProviderTest>();
}
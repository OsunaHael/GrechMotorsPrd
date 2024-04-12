using CurrieTechnologies.Razor.SweetAlert2;
using GrechMotorsPrd.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using QRCoder;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Registra QrCodeGenerator como un servicio
builder.Services.AddSingleton<QrCodeGenerator>();

builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
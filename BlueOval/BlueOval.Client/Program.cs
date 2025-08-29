// Client/Program.cs

using Append.Blazor.Printing;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// --- Register HttpClient ---
// The base address should be the address of your server project.
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// --- UI and Client-Side Services ---
builder.Services.AddMudServices();

builder.Services.AddScoped<IPrintingService, PrintingService>();

builder.Services.AddScoped<DapperContextApi>();
builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<ApiAuthenticationStateProvider>());

builder.Services.AddCascadingAuthenticationState();
// --- Client-Side Authentication ---
builder.Services.AddAuthorizationCore();
// You will need a custom AuthenticationStateProvider
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();
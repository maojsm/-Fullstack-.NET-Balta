using System.Globalization;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Dima.Web;
using Dima.Web.Handlers;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Dima.Web.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CookieHandler>();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddScoped(x =>
    (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddMudServices();

builder.Services
    .AddHttpClient(Configuration.HttpClientName, 
    opt => 
    { 
        opt.BaseAddress = new Uri(Configuration.BackendUrl); 
    })
    .AddHttpMessageHandler<CookieHandler>();


builder.Services.AddTransient<IAccountHandler, AccountHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<IReportHandler, ReportHandler>();
builder.Services.AddTransient<IOrderHandler, OrderHandler>();
builder.Services.AddTransient<IStripeHandler, StripeHandler>();
builder.Services.AddTransient<ITrafficControllerHandler, TrafficControlerHandler>();
builder.Services.AddTransient<ITcHardwareInRealtimeHandler, TcHardwareInRealtimeHandler>();

// Coloca a pagina em portugues paara o navegador.
builder.Services.AddLocalization();
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

// Configure the cert and the key (SSL)
//builder.Configuration["Kestrel:Certificates:Default:Path"] = $"ssl_cert_apontamentoonline.pem";
//builder.Configuration["Kestrel:Certificates:Default:KeyPath"] = $"ssl_key_apontamentoonline.pem";

await builder.Build().RunAsync();
using HARIA.Data.DependencyInjection;
using HARIA.Domain.DependencyInjection;
using HARIA.Services.DependencyInjection;
using HARIA.StateKeeper.Abstractions;
using HARIA.StateKeeper.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHariaConfiguration();
builder.Services.AddHariaServices();
builder.Services.AddHariaData();
builder.Services.AddHariaDomain();
builder.Services.AddServices();
builder.Services.ConfigureLogger();
var app = builder.Build();

app.Urls.Add($"http://*:{app.Configuration["STATE_KEEPER_PORT"] ?? "7020"}");
var service = app.Services.GetService<IHariaStateKeeperService>();
service?.InitializeService();

app.MapGet("/state", () => "online");

app.Run();

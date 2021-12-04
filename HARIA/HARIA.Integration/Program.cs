using HARIA.Data.DependencyInjection;
using HARIA.Domain.DependencyInjection;
using HARIA.Integration.Abstractions;
using HARIA.Integration.DependencyInjection;
using HARIA.Integration.Extenssions;
using HARIA.Services.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHariaConfiguration();
builder.Services.AddHariaServices();
builder.Services.AddHariaData();
builder.Services.AddHariaDomain();
builder.Services.AddServices();
builder.Services.ConfigureLogger();
builder.AddSwagger();
var app = builder.Build();

app.Urls.Add($"http://*:{app.Configuration["INTEGRATION_PORT"] ?? "7030"}");
var service = app.Services.GetService<IHariaIntegrationService>();
if (service == null) throw new Exception("Initialization fail.");

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/actuators",
    async ([FromQueryAttribute(Name = "api_key")] string apiKey,
           HttpContext http) =>
    {
        if (!app.ValidateApiKey(http, apiKey))
            return null;            
        return await service.GetActuators();
    });

app.MapGet("/actuator/{deviceId}/{itemId}",
    async ([FromRoute] string deviceId,
           [FromRoute] string itemId,
           [FromQueryAttribute(Name = "api_key")] string apiKey,
           HttpContext http) =>
    {
        if (!app.ValidateApiKey(http, apiKey))
            return null;
        return await service.GetActuator(deviceId, itemId);
    });

app.MapGet("/sensors",
    async ([FromQueryAttribute(Name = "api_key")] string apiKey, HttpContext http) =>
    {
        if (!app.ValidateApiKey(http, apiKey))
            return null;
        return await service.GetSensors();
    });

app.MapGet("/sensor/{deviceId}/{itemId}",
    async ([FromRoute] string deviceId,
           [FromRoute] string itemId,
           [FromQueryAttribute(Name = "value")] bool value,
           [FromQueryAttribute(Name = "api_key")] string apiKey,
           HttpContext http) =>
    {
        if (!app.ValidateApiKey(http, apiKey))
            return null;
        return await service.GetSensor(deviceId, itemId);
    });


app.MapGet("set/actuator/{deviceId}/{itemId}",
    async ([FromRoute] string deviceId,
           [FromRoute] string itemId,
           [FromQueryAttribute(Name = "value")] bool value,
           [FromQueryAttribute(Name = "api_key")] string apiKey,
           HttpContext http) =>
    {
        if (!app.ValidateApiKey(http, apiKey))
            return;
        await service.SetActuator(deviceId, itemId, value);
    });


app.Run();

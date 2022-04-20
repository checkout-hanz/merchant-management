using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Google.Api.Gax;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Auth;
using Grpc.Core;
using Merchant.Management.Configuration;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:8080");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
var gcpSettings = builder.Configuration.BindConfigurationSection<GcpSettings>("GCP");
builder.Services.AddSingleton<IGcpSettings>(gcpSettings);

builder.Services
    .AddMongo(builder.Configuration, builder.Environment)
    .AddMessaging()
    .AddMappers()
    .AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.


const string healthCheckEndpointPath = "/api/health";
app.MapHealthChecks(healthCheckEndpointPath);

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public interface IGcpSettings
{
    string ProjectId { get; set; }
    string ServiceName { get; set; }
}

public class GcpSettings : IGcpSettings
{
    public string ProjectId { get; set; }

    public string ServiceName { get; set; }
}

public static class ConfigurationExtension
{
    public static T BindConfigurationSection<T>(this IConfiguration configuration, string section, params object[] constructorArgs) where T : class
    {
        var settings = Activator.CreateInstance(typeof(T), constructorArgs);
        configuration.GetSection(section).Bind(settings);
        return settings as T;
    }
}

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(
                $"Error executing the request.");
        }
    }
}

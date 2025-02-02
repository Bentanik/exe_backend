using exe_backend.Infrastructure.DepedencyInjection.Extensions;
using exe_backend.Persistence.DepedencyInjection.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

// Register ASP version
builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

// Register MediatR
builder.Services.AddConfigureMediatR();

// Register Middlewares
builder.Services.AddScoped<ExceptionHandlingMiddleware>();

// Register SQL
builder.Services.AddSqlConfiguration();

// Register Redis
builder.Services.AddConfigurationRedis(builder.Configuration);

// Register HealthChecks
builder.Services.AddHealthChecks()
     .AddSqlServer(builder.Configuration.GetConnectionString("Database")!)
     .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

// Register Persistence services
builder.Services.AddPersistenceService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.NewVersionedApi("Authentication")
    .MapAuthApiV1();

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.UseHttpsRedirection();
app.Run();

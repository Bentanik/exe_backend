using exe_backend.Api.DepedencyInjection.Extensions;
using exe_backend.Application.Workers;
using exe_backend.Infrastructure.DepedencyInjection.Extensions;
using exe_backend.Persistence.DepedencyInjection.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Claims;

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

// Configure appsettings
builder.Services.AddConfigurationAppSetting(builder.Configuration);

// Configure Auth
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

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

// Register Mapping config
builder.Services.AddMappingConfig();

// Register Persistence services
builder.Services.AddPersistenceServices();

// Register Infrastructure service
builder.Services.AddInfrastructureServices(builder.Configuration);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("http://localhost:3000", "https://exe02-fe-web.vercel.app")
                    .AllowAnyHeader().AllowAnyMethod().AllowCredentials());
});

// Configuration send file from form
builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = 400 * 1024 * 1024; // 400MB
    options.MultipartBodyLengthLimit = 400 * 1024 * 1024; // 400MB
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 400 * 1024 * 1024; // 400MB
});


// Configuration Worker subscription
//builder.Services.AddHostedService<SubscriptionCleanupWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Seed database
    await app.InitialiseDatabaseAsync(builder.Configuration, builder.Services.BuildServiceProvider());
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/antiscm-helloworld", (HttpContext context) =>
{
    var user = context.User.FindFirstValue("UserId");
    return Results.Ok(user);
}).RequireAuthorization("Member");

app.MapCarter();

app.NewVersionedApi("Authentication")
    .MapAuthApiV1();

app.NewVersionedApi("Course")
    .MapCourseApiV1();

app.NewVersionedApi("Category")
    .MapCategoryApiV1();

app.NewVersionedApi("Level")
    .MapLevelApiV1();

app.NewVersionedApi("User")
    .MapUserApiV1();

app.NewVersionedApi("Donate")
    .MapDonateApiV1();

app.NewVersionedApi("Subscription")
    .MapSubscriptionApiV1();

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

// CORS
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.Run();

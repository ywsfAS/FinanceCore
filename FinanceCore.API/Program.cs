using Asp.Versioning;
using Asp.Versioning.Conventions;
using FinanceCore.API;
using FinanceCore.API.Configuration;
using FinanceCore.API.Services;
using FinanceCore.Application;
using FinanceCore.Application.Abstractions;
using FinanceCore.Infrastructure;
using FinanceCore.Infrastructure.Health;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpsRedirection(policy =>
{
    policy.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
});

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
}).AddMvc(config =>
{
    config.Conventions.Add(new VersionByNamespaceConvention());
}).AddApiExplorer(config =>
{
    config.GroupNameFormat = "'v'V";
    config.SubstituteApiVersionInUrl = true;
});

builder.Services.AddOptions<OpenTelemetryOptions>()
    .BindConfiguration("OpenTelemetry")
    .ValidateDataAnnotations()
    .ValidateOnStart();
var oltpUrl = builder.Configuration["OpenTelemetry:OtlpEndpoint"];
builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing
            .AddSqlClientInstrumentation()
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri(oltpUrl!);
            });
    }
    )
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri(oltpUrl!);
            }); 

    });



builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

builder.Services.AddApplication();
builder.Services.AddScoped<IRequestMetadata, RequestMetadata>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FinanceCore API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {token}'"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var allowedOrigins = builder.Configuration
    .GetSection("AllowedCorsOrigins")
    .Get<string[]>() ?? [];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


builder.Services
    .AddOptions<RateLimitingOptions>()
    .BindConfiguration("RateLimiting")
    .ValidateDataAnnotations()
    .ValidateOnStart();

var rateLimitOptions = builder.Configuration
    .GetSection("RateLimiting")
    .Get<RateLimitingOptions>()!;

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddFixedWindowLimiter("Auth", options =>
    {
        options.Window = TimeSpan.FromMinutes(
            rateLimitOptions.Authentication.WindowInMinutes);

        options.PermitLimit =
            rateLimitOptions.Authentication.PermitLimit;
    });

    options.AddSlidingWindowLimiter("Default", options =>
    {
        options.PermitLimit =
            rateLimitOptions.Default.PermitLimit;

        options.Window = TimeSpan.FromMinutes(
            rateLimitOptions.Default.WindowInMinutes);

        options.SegmentsPerWindow =
            rateLimitOptions.Default.SegmentsPerWindow;
    });

    options.AddConcurrencyLimiter("Reports", options =>
    {
        options.PermitLimit =
            rateLimitOptions.Reporting.PermitLimit;

        options.QueueLimit =
            rateLimitOptions.Reporting.QueueLimit;
    });
});


builder.Services.AddSerilog(
    configuration => configuration
        .ReadFrom.Configuration(builder.Configuration));

var app = builder.Build();

app.UseGlobalException();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseSerilogRequestLogging();

app.UseCors("AllowFrontend");

app.UseStaticFiles();

app.UseAuthentication();

app.UseRateLimiter();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health/live");

app.MapHealthChecks(
    "/health/ready",
    new HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("ready"),
        ResponseWriter = WriteHeathJsonReport.WriteHealthCheckResponse
    });

app.MapControllers();

app.Run();

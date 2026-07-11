using FinanceCore.API;
using FinanceCore.API.Configuration;
using FinanceCore.Application;
using FinanceCore.Infrastructure;
using FinanceCore.Infrastructure.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinanceCore API", Version = "v1" });

    // Define the security scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {token}'",
    });

    // Require JWT for all endpoints in Swagger
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] { }
        }
    });
});
// Add CQRS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // React dev server
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
var rateLimitOptions = builder.Configuration
    .GetSection("RateLimiting")
    .Get<RateLimitingOptions>()!;
// Add Rate Limiting 
builder.Services.AddRateLimiter(options => {
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddFixedWindowLimiter("Auth",opt =>
    {
       
        opt.Window = TimeSpan.FromMinutes(rateLimitOptions.Authentication.WindowInMinutes);
        opt.PermitLimit = rateLimitOptions.Authentication.PermitLimit;
    });

    options.AddSlidingWindowLimiter("Default", opt =>
    {
        opt.PermitLimit = rateLimitOptions.Default.PermitLimit;
        opt.Window = TimeSpan.FromMinutes(rateLimitOptions.Default.WindowInMinutes);
        opt.SegmentsPerWindow = rateLimitOptions.Default.SegmentsPerWindow;

    });

    options.AddConcurrencyLimiter("Reports", opt =>
    {
        opt.PermitLimit = rateLimitOptions.Reporting.PermitLimit;
        opt.QueueLimit = rateLimitOptions.Reporting.QueueLimit;
    });

});
var app = builder.Build();
app.UseGlobalException();
app.UseCors("AllowFrontend");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();



app.UseStaticFiles();
app.UseAuthentication();
app.UseRateLimiter();
app.UseAuthorization();

app.MapControllers();

app.Run();

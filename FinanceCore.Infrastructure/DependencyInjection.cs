using FinanceCore.Application.Abstractions;
using FinanceCore.Infrastructure.Auth;
using FinanceCore.Infrastructure.BackgroundJobs;
using FinanceCore.Infrastructure.Context;
using FinanceCore.Infrastructure.Health;
using FinanceCore.Infrastructure.Persistence;
using FinanceCore.Infrastructure.Repositories;
using FinanceCore.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Text;

namespace FinanceCore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.Configure<ExchangeRateApiSettings>(
            config.GetSection("ExchangeRateApi"));
            // Recurring Transactions Job Config + Sync Exchange Rates Job Config 
            services.AddJobWithTrigger<RecurringTransactionJob>("RecurringTransactionJob", "RecurringTranactionTrigger", 2);
            services.AddJobWithTrigger<ExchangeRateSyncJob>("ExchangeRateJob", "ExchangeRateTrigger", 6);
            services.AddQuartzHostedService(config => config.WaitForJobsToComplete = true);

            var connectionString =
                config.GetConnectionString("DefaultConnection");

            if (connectionString == null)
                throw new InvalidOperationException("No connection string found.");

            services.AddScoped<IConnectionFactory>(sp =>
            new SqlConnectionFactory(connectionString));

            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IImageStorage, LocalImageStorage>();
            services.AddMemoryCache();
            services.AddScoped<AccountRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<TransactionRepository>();
            services.AddScoped<BudgetRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<IAccountRepository,CacheAccountRepository>();
            services.AddScoped<ICategoryRepository, CacheCategoryRepository>();
            services.AddScoped<IBudgetRepository, CacheBudgetRepository>();
            services.AddScoped<IUserRepository, CacheUserRepository>();
            services.AddScoped<ITransactionRepository, CacheTransactionRepository>();
            services.AddScoped<IAuditRepository, AuditLogRepository>();
            services.AddScoped<IRecurringTransactionRepository , RecurringTransactionRepository>();
            services.AddScoped<ISavingsGoalRepository,SavingsGoalRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IPasswordResetTokenRepository,PasswordResetTokenRepository>();
            services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ITransactionExporter, TransactionExporter>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ICurrencyConverter, CurrencyConverter>();
            services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
            services.AddHttpClient<IExchangeRateApiService, ExchangeRateApiService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
            services.AddScoped<IImageProcessor, ImageProcessor>();

            services.AddHealthChecks().AddCheck<DatabaseHealthCheck>("database",HealthStatus.Unhealthy, tags : new[] {"ready"});
                

            services.Configure<JwtSettings>(
                config.GetSection("JwtSettings"));

            var jwtSettings =
                config.GetSection("JwtSettings")
                      .Get<JwtSettings>();
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings!.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    };
            });
            var emailSection = config.GetSection("EmailSettings");
            services.Configure<EmailSettings>(emailSection);

            var frontendSettingsSection = config.GetSection("Frontend");
            services.Configure<FrontendOptions>(frontendSettingsSection);

            services.AddSingleton<IFrontendSettingsProvider, FrontendSettingsProvider>();

            return services;
        }
    }
}
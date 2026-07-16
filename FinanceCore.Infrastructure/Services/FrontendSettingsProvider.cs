using FinanceCore.Application.Abstractions;
using Microsoft.Extensions.Options;

namespace FinanceCore.Infrastructure.Services
{
    public class FrontendSettingsProvider : IFrontendSettingsProvider
    {
        private readonly FrontendOptions _options;
        public FrontendSettingsProvider(IOptions<FrontendOptions> options)
        {
            _options = options.Value;
        }
        public string FrontendBaseUrl => _options.FrontendBaseUrl;

    }
}

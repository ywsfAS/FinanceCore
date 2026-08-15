using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Events.Account;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinanceCore.Application.Events.Accounts;

public class AccountBalanceChangedHandler
    : INotificationHandler<AccountBalanceChangedEvent>
{
    private readonly ILogger<AccountBalanceChangedHandler> _logger;
    private readonly IAlertRepository _alertRepository;

    public AccountBalanceChangedHandler(
        ILogger<AccountBalanceChangedHandler> logger,
        IAlertRepository alertRepository)
    {
        _logger = logger;
        _alertRepository = alertRepository;
    }

    public async Task Handle(
        AccountBalanceChangedEvent @event,
        CancellationToken token)
    {
        var alerts = await _alertRepository.GetActiveAlertsAsync(
            @event.AccountId,
            token);

        foreach (var alert in alerts)
        {
            if (alert.ShouldTrigger(
                    @event.NewBalance.Amount))
            {
                alert.Trigger(DateTime.UtcNow);

                await _alertRepository.UpdateAsync(
                    alert,
                    token);

                _logger.LogInformation(
                    "Low balance alert {AlertId} triggered for account {AccountId}. " +
                    "Balance changed from {PreviousBalance} to {NewBalance} Currency {Currency}. " +
                    "Threshold: {Threshold}",
                    alert.Id,
                    @event.AccountId,
                    @event.PreviousBalance.Amount,
                    @event.NewBalance.Amount,
                    alert.ThresholdAmount,
                    @event.NewBalance.Currency);
            }
        }
    }
}

using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Events.Goal;
using FinanceCore.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Events.SavingGoals.GoalCanceled
{
    public class SendEmailHandler : INotificationHandler<GoalCancelledEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        public SendEmailHandler(IEmailService emailService, IUserRepository userRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
        }
        public async Task Handle(GoalCancelledEvent notification, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetByIdAsync(notification.UserId);
            if (user is null)
                throw new UserNotFoundException(notification.UserId);
            var Email = user.Email;
            var Subject = $"Your savings goal {notification.Name} was cancelled";
            var Body = $"Your savings goal has been cancelled at {notification.CurrentAmount.Amount}{notification.CurrentAmount.Currency}.\r\n\r\nYou can always create a new one whenever you're ready to continue your financial journey.";
            await _emailService.SendEmailAsync(Email, Subject, Body);
        }
    }
}

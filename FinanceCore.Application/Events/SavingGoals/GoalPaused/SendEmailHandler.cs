using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Events.Goal;
using FinanceCore.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Events.SavingGoals.GoalPaused
{
    public class SendEmailHandler : INotificationHandler<GoalPausedEvent>
    {
        private readonly IEmailService _emailService;
        private readonly ISavingsGoalRepository _savingGoalRepository;
        private readonly IUserRepository _userRepository;
        public SendEmailHandler(IEmailService emailService, ISavingsGoalRepository savingsGoalRepository, IUserRepository userRepository)
        {
            _emailService = emailService;
            _savingGoalRepository = savingsGoalRepository;
            _userRepository = userRepository;
        }
        public async Task Handle(GoalPausedEvent notification, CancellationToken cancellationToken)
        {
            var goal = await _savingGoalRepository.GetByIdAsync(notification.GoalId, cancellationToken);
            if (goal is null)
                throw new GoalNotFoundException(notification.GoalId);

            var user = await _userRepository.GetByIdAsync(goal.UserId);
            if (user is null)
                throw new UserNotFoundException(goal.UserId);
            var Email = user.Email;
            var Subject = $"Your savings goal {notification.Name} is paused";
            var Body = $"Your savings goal is currently paused.\r\n\r\nNo progress will be tracked until you resume it. You can restart it anytime.";
            await _emailService.SendEmailAsync(Email, Subject, Body);
        }
    }
}

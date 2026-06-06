using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Goals.Commands.Create;
using FinanceCore.Domain.Events.User;
using FinanceCore.Domain.Events.Goal;
using FinanceCore.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceCore.Domain.Common;

namespace FinanceCore.Application.Events.SavingGoals.GoalCreated
{
    public class SendEmailHandler : INotificationHandler<GoalCreatedEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        public SendEmailHandler(IEmailService emailService,IUserRepository userRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
        }
       public async Task Handle(GoalCreatedEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(notification.UserId);
            if(user is null)
            {
                throw new UserNotFoundException(notification.UserId);
            }
            var Email = user.Email;
            var Subject = $"Your savings goal is {notification.Name} ready";
            var Body = $"Great start! Your savings goal has been created successfully.Every step you take brings you closer to your target. Stay consistent progress builds over time.";
            await _emailService.SendEmailAsync(Email, Subject, Body);
        }
    }
}

using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Profiles.Commands.Create
{
    public record CreateProfileCommand(Guid userId, string firstName, string lastName, string bio , string? avatarUrl , EnCurrency curreny) : IRequest<ProfileDto>;


    
}

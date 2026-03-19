using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Profiles.Commands.Delete
{
     public record DeleteProfileCommand(Guid id) : IRequest;
    
    
}

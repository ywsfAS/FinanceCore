using FinanceCore.Application.Models;
using FinanceCore.Domain.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Abstractions
{
    public interface IProfileRepository
    {
        Task<ProfileModel?> GetProfileByUserIdAsync(Guid userId);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<ProfileModel>> GetAllAsync();
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Profile profile , CancellationToken token);

        Task AddAsync(Profile profile , CancellationToken token);
    }
}

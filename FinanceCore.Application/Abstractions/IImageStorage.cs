using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Abstractions
{
    public interface IImageStorage
    {
        Task<string> SaveImage(Stream stream , string filename , Guid id);
    }
}

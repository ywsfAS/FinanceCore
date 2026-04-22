using FinanceCore.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Services
{
    public class LocalImageStorage : IImageStorage
    {
        public async Task<string> SaveImage(Stream stream , string filename , Guid id)
        {
            var folder = Path.Combine("wwwroot","uploads", "users", "profiles");
            Directory.CreateDirectory(folder);
            var newFilename = $"{id}_{Guid.NewGuid()}_{Path.GetExtension(filename)}";
            var path = Path.Combine(folder,newFilename);
            using var file = new FileStream(path, FileMode.Create,FileAccess.Write);
            await stream.CopyToAsync(file);
            return newFilename;

        }
    }
}

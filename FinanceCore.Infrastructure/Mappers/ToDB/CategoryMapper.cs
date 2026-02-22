using FinanceCore.Domain.Categories;
using FinanceCore.Infrastructure.Models.FinanceCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers.ToDB
{
    public static class CategoryMapper
    {
        public static CategoryModel MapToModel(Category category)
        {
            return new CategoryModel {Id = category.Id , UserId = category.UserId , Name = category.Name , IsActive = category.IsActive , Type = (byte)category.Type , Description = category.Description , IsDefault = category.IsDefault , CreatedAt = category.CreatedAt , UpdatedAt = category.UpdatedAt };
        }
    }
}

using FinanceCore.Domain.Categories;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Models.FinanceCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryModel MapToModel(Category category)
        {
            return new CategoryModel {Id = category.Id , UserId = category.UserId , Name = category.Name , IsActive = category.IsActive , CategoryTypeId = (byte)category.Type , Description = category.Description , IsDefault = category.IsDefault , CreatedAt = category.CreatedAt , UpdatedAt = category.UpdatedAt };
        }
        public static Category MapToDomain(CategoryModel model)
        {
            return Category.Create(model.Id, model.UserId, model.Name, (CategoryType)model.CategoryTypeId,model.IsActive,model.IsDefault,model.Description, model.CreatedAt , model.UpdatedAt);
        }
    }
}

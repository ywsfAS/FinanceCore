using FinanceCore.Domain.Categories;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Models.FinanceCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers.ToDomain
{
    public  static class CategoryMapper
    {
        public static Category MapToDomain(CategoryModel model)
        {
            return Category.Create(model.Id,model.UserId,model.Name,(CategoryType)model.Type,model.CreatedAt,model.IsActive,model.Description,model.IsDefault);


        }
    }
}

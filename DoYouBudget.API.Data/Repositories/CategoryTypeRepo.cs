using DoYouBudget.API.Data.Base;
using DoYouBudget.API.Data.Context;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoYouBudget.API.Data.Repositories
{
    public class CategoryTypeRepo : BaseRepo<DoYouBudgetContext>, ICategoryTypeRepo
    {
        private readonly DoYouBudgetContext _context;
        public CategoryTypeRepo(DoYouBudgetContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryTypeModel>> GetCategoryType()
        {
            return await _context.CategoryType.ToListAsync();
        }
    }
}

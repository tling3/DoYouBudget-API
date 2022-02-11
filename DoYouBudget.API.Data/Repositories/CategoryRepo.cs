using DoYouBudget.API.Data.Base;
using DoYouBudget.API.Data.Context;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoYouBudget.API.Data.Repositories
{
    public class CategoryRepo : BaseRepo<DoYouBudgetContext>, ICategoryRepo
    {
        private readonly DoYouBudgetContext _context;

        public CategoryRepo(DoYouBudgetContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryModel>> GetCategories()
        {
            return await _context.Category.ToListAsync();
        }
    }
}

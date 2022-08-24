using DoYouBudget.API.Data.Base;
using DoYouBudget.API.Data.Context;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
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
            IEnumerable<CategoryModel> domain = await _context.Category.ToListAsync();
            return domain;
        }

        public async Task<CategoryModel> GetCategoryById(int id)
        {
            CategoryModel domain = await _context.Category.FindAsync(id);
            return domain;
        }

        public async Task<bool> InsertCategory(CategoryModel domain)
        {
            if (domain == null)
                throw new ArgumentNullException(nameof(domain));
            await _context.AddAsync(domain);
            bool isSuccessful = SaveChanges();
            return isSuccessful;
        }
    }
}

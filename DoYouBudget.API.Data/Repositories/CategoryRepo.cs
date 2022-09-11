using DoYouBudget.API.Data.Base;
using DoYouBudget.API.Data.Context;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using DoYouBudget.API.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<CategoryReadDto> GetCategories()
        {
            List<CategoryReadDto> domain = _context.Category
                .Join(
                    _context.CategoryType,
                    category => category.TypeId,
                    type => type.Id,
                    (category, type) => new CategoryReadDto()
                    {
                        Id = category.Id,
                        Type = type.Type,
                        UserId = category.UserId,
                        Category = category.Category,
                        Budget = category.Budget,
                        TypeId = category.TypeId,
                        PostDate = category.PostDate,
                        ModifiedBy = category.ModifiedBy,
                        CreatedDate = category.CreatedDate,
                        ModifiedDate = category.ModifiedDate
                    }
                ).ToList();
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

        public void UpdateCategory(CategoryModel domain)
        {
            // intentionally left blank
        }

        public bool DeleteCategory(CategoryModel domain)
        {
            if (domain == null)
                throw new ArgumentNullException();

            _context.Remove(domain);
            bool isSuccessful = SaveChanges();
            return isSuccessful;
        }
    }
}

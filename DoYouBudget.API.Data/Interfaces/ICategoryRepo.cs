using DoYouBudget.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoYouBudget.API.Data.Interfaces
{
    public interface ICategoryRepo
    {
        Task<IEnumerable<CategoryModel>> GetCategories();
        Task<CategoryModel> GetCategoryById(int id);
        Task<bool> InsertCategory(CategoryModel domain);
        void UpdateCategory(CategoryModel domain);
        bool DeleteCategory(CategoryModel domain);
        bool SaveChanges();
    }
}

using DoYouBudget.API.Models.Domain;
using DoYouBudget.API.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoYouBudget.API.Data.Interfaces
{
    public interface ICategoryRepo
    {
        IEnumerable<CategoryReadDto> GetCategories();
        Task<CategoryModel> GetCategoryById(int id);
        Task<bool> InsertCategory(CategoryModel domain);
        void UpdateCategory(CategoryModel domain);
        bool DeleteCategory(CategoryModel domain);
        bool SaveChanges();
    }
}

using DoYouBudget.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoYouBudget.API.Data.Interfaces
{
    public interface ICategoryTypeRepo
    {
        Task<IEnumerable<CategoryTypeModel>> GetCategoryType();
    }
}

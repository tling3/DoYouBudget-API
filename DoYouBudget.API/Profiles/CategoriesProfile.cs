using AutoMapper;
using DoYouBudget.API.Models.Domain;
using DoYouBudget.API.Models.Dto;

namespace DoYouBudget.API.Profiles
{
    /// <summary>
    /// Categories Mapper Profile
    /// </summary>
    public class CategoriesProfile : Profile
    {
        /// <summary>
        /// Create Maps in constructor
        /// </summary>
        public CategoriesProfile()
        {
            CreateMap<CategoryModel, CategoryReadDto>();
        }
    }
}

using AutoMapper;
using DoYouBudget.API.Models.Domain;
using DoYouBudget.API.Models.Dto;

namespace DoYouBudget.API.Profiles
{
    /// <summary>
    /// CategoryType Mapper Profile
    /// </summary>
    public class CategoryTypeProfile : Profile
    {
        /// <summary>
        /// Create Maps
        /// </summary>
        public CategoryTypeProfile()
        {
            CreateMap<CategoryTypeModel, CategoryTypeReadDto>();
        }
    }
}

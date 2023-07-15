using AutoMapper;
using DoYouBudget.API.Models.Domain;
using DoYouBudget.API.Models.Dto;

namespace DoYouBudget.API.Profiles
{
    /// <summary>
    /// Monthly Log Mapper Profile
    /// </summary>
    public class MonthlyLogProfile : Profile
    {
        /// <summary>
        /// Create Maps
        /// </summary>
        public MonthlyLogProfile()
        {
            CreateMap<MonthlyLogModel, MonthlyLogReadDto>()
                .ForMember(targ => targ.Category, opt => opt.MapFrom(src => src.Category.Category));
            CreateMap<MonthlyLogInsertDto, MonthlyLogModel>()
                .ForMember(targ => targ.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
            CreateMap<MonthlyLogUpdateDto, MonthlyLogModel>();
        }
    }
}

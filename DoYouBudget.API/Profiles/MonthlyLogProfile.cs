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
            CreateMap<MonthlyLogModel, MonthlyLogReadDto>();
            CreateMap<MonthlyLogModel, MonthlyLogReadDto>();
            CreateMap<MonthlyLogInsertDto, MonthlyLogModel>();
        }
    }
}

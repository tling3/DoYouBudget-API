using AutoMapper;
using DoYouBudget.API.Models.Domain;
using DoYouBudget.API.Models.Dto;

namespace DoYouBudget.API.Profiles
{
    /// <summary>
    /// TODO:
    /// </summary>
    public class EventLocationProfile : Profile
    {
        /// <summary>
        /// TODO:
        /// </summary>
        public EventLocationProfile()
        {
            CreateMap<EventLocationModel, EventLocationReadDto>();
            CreateMap<EventLocationReadDto, EventLocationModel>();
            CreateMap<EventInsertDto, EventModel>();
        }
    }
}

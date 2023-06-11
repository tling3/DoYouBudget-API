using AutoMapper;
using DoYouBudget.API.Models.Domain;
using DoYouBudget.API.Models.Dto;

namespace DoYouBudget.API.Profiles
{
    /// <summary>
    /// TODO:
    /// </summary>
    public class EventProfile : Profile
    {
        /// <summary>
        /// TODO:
        /// </summary>
        public EventProfile()
        {
            CreateMap<EventModel, EventReadDto>();
            CreateMap<EventReadDto, EventModel>();
        }
    }
}

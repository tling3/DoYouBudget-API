using AutoMapper;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using DoYouBudget.API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace DoYouBudget.API.Controllers
{
    /// <summary>
    /// TODO:
    /// </summary>
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class EventController : ControllerBase
    {
        readonly IEventRepo _repository;
        readonly IMapper _mapper;
        readonly IEventLocationRepo _locationRepo;
        /// <summary>
        /// TODO:
        /// </summary>
        public EventController(
            IEventRepo repository,
            IMapper mapper,
            IEventLocationRepo locationRepo)
        {
            _repository = repository;
            _mapper = mapper;
            _locationRepo = locationRepo;
        }

        /// <summary>
        /// TODO:
        /// </summary>
        /// <returns></returns>
        [HttpGet("events")]
        public async Task<ActionResult<IEnumerable<EventReadDto>>> GetEvents()
        {
            IEnumerable<EventModel> domains = await _repository.GetEvents();
            if (domains == null)
                return NotFound();

            IEnumerable<EventReadDto> dto = _mapper.Map<IEnumerable<EventReadDto>>(domains);
            return Ok(dto);
        }

        /// <summary>
        /// TODO:
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("events/{id}", Name = nameof(GetEventById))]
        public async Task<ActionResult<EventModel>> GetEventById(int id)
        {
            EventModel domain = await _repository.GetEventById(id);
            if (domain == null)
                return NotFound();

            EventLocationModel locationDomain = await _locationRepo.GetEventLocationById(domain.LocationId);
            EventReadDto dto = _mapper.Map<EventReadDto>(domain);
            EventLocationReadDto locationDto = _mapper.Map<EventLocationReadDto>(locationDomain);
            dto.Location = locationDto;
            return Ok(dto);
        }

        /// <summary>
        /// TODO:
        /// </summary>
        /// <param name="eventDto"></param>
        /// <returns></returns>
        [HttpPost("events")]
        public async Task<ActionResult> InsertEvent(EventInsertDto eventDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            EventLocationReadDto locationDto = eventDto.Location;
            EventLocationModel locationDomain = _mapper.Map<EventLocationModel>(locationDto);
            locationDomain.ModifiedBy = "TL";
            EventModel eventDomain = _mapper.Map<EventModel>(eventDto);
            eventDomain.ModifiedBy = "TL";

            bool isEventSuccessful = false;
            bool isLocationSuccessful = false;
            int locationId;
            var transactionScope = new TransactionScope();
            using (transactionScope)
            {
                (isLocationSuccessful, locationId) = await _locationRepo.InsertEventLocation(locationDomain);
                eventDomain.LocationId = locationId;
                isEventSuccessful = await _repository.InsertEvent(eventDomain);
                transactionScope.Complete();
            }

            if (!(isEventSuccessful && isLocationSuccessful))
                return StatusCode(StatusCodes.Status500InternalServerError);

            EventReadDto eventReadDto = _mapper.Map<EventReadDto>(eventDomain);
            locationDto = _mapper.Map<EventLocationReadDto>(locationDomain);
            eventReadDto.Location = locationDto;

            return CreatedAtRoute(nameof(GetEventById), new { id = eventReadDto.Id }, eventReadDto);
        }
    }
}

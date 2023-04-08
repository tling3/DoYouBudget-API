using AutoMapper;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using DoYouBudget.API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoYouBudget.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/monthlyLogs")]
    [Produces("application/json")]
    [ApiController]
    public class MonthlyLogController : ControllerBase
    {
        private readonly IMonthlyLogRepo _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inject Mapper and Repository
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public MonthlyLogController(IMonthlyLogRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/monthlyLogs/{id}
        /// <summary>
        /// Get MonthlyLog record by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet("{userId}/{month}", Name = nameof(GetMonthlyLogsByUserId))]
        public ActionResult<IEnumerable<MonthlyLogReadDto>> GetMonthlyLogsByUserId(int userId, int month)
        {
            IEnumerable<MonthlyLogModel> domain = _repository.GetMonthlyLogsByUserId(userId, month);
            if (domain == null)
                return NotFound();
            IEnumerable<MonthlyLogReadDto> dto = _mapper.Map<IEnumerable<MonthlyLogReadDto>>(domain);
            return Ok(dto);
        }

        // GET api/monthlyLog/{id}
        /// <summary>
        /// Get MonthlyLog by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<MonthlyLogReadDto>> GetMonthlyLogById(int id)
        {
            MonthlyLogModel domain = await _repository.GetMonthlyLogById(id);
            if (domain == null)
                return NotFound();
            MonthlyLogReadDto dto = _mapper.Map<MonthlyLogReadDto>(domain);
            return Ok(dto);
        }

        // POST api/monthlyLog
        /// <summary>
        /// Insert MonthlyLog record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<int>> InsertMonthlyLogById(MonthlyLogInsertDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            MonthlyLogModel domain = _mapper.Map<MonthlyLogModel>(dto);
            bool isSuccessful = await _repository.InsertMonthlyLog(domain);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);
            MonthlyLogReadDto readDto = _mapper.Map<MonthlyLogReadDto>(domain);

            return CreatedAtRoute(nameof(GetMonthlyLogsByUserId), new { userId = readDto.Id, month = 10 }, readDto);
        }

        // TODO: ADD ROUTE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMonthlyLogById(MonthlyLogUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            MonthlyLogModel domain = await _repository.GetMonthlyLogById(updateDto.Id);
            if (domain == null)
                return NotFound();

            _mapper.Map(updateDto, domain);
            _repository.UpdateMonthlyLogById(domain);
            bool isSuccessful = _repository.SaveChanges();
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }

        // TODO: add route and swagger comment
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMonthlyLog(int id)
        {
            MonthlyLogModel domain = await _repository.GetMonthlyLogById(id);
            if (domain == null)
                return NotFound();

            bool isSuccessful = _repository.DeleteMonthlyLog(domain);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }
    }
}

using AutoMapper;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using DoYouBudget.API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoYouBudget.API.Controllers
{
    /// <summary>
    /// Provide monthly logs data
    /// </summary>
    [Route("api/monthlyLogs")]
    [Produces("application/json")]
    [ApiController]
    public class MonthlyLogController : ControllerBase
    {
        private readonly IMonthlyLogRepo _repository;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inject Mapper and Repository
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public MonthlyLogController(
            IMonthlyLogRepo repository,
            ICategoryRepo categoryRepo,
            IMapper mapper)
        {
            _repository = repository;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        // GET api/monthlyLogs/{id}
        /// <summary>
        /// Get MonthlyLog record by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="month"></param>
        /// <returns>Monthly logs by user id</returns>
        /// <response code="404">Monthly logs not found for user</response>
        /// <response code="200">Monthly logs successfully found for user</response>
        [HttpGet("{userId}/{month}", Name = nameof(GetMonthlyLogsByUserId))]
        public async Task<ActionResult<IEnumerable<MonthlyLogReadDto>>> GetMonthlyLogsByUserId(int userId, int month)
        {
            IEnumerable<MonthlyLogModel> domain = await _repository.GetMonthlyLogsByUserId(userId, month);
            IEnumerable<CategoryModel> categoryDomain = await _categoryRepo.GetCategories();

            if (domain == null || categoryDomain == null)
                return NotFound();

            List<MonthlyLogReadDto> dto = domain
                .Join(
                    categoryDomain,
                    domain => domain.CategoryId,
                    category => category.Id,
                    (domain, category) => new MonthlyLogReadDto()
                    {
                        Id = domain.Id,
                        UserId = domain.UserId,
                        Amount = domain.Amount,
                        CategoryId = category.Id,
                        Category = category.Category,
                        TransactionDate = domain.TransactionDate,
                        Comment = domain.Comment,
                        Month = domain.Month,
                        CreatedDate = domain.CreatedDate,
                        ModifiedDate = domain.ModifiedDate,
                        ModifiedBy = domain.ModifiedBy,
                    }
                ).ToList();

            return Ok(dto);
        }

        // GET api/monthlyLog/{id}
        /// <summary>
        /// Get MonthlyLog by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Monthly log record</returns>
        /// <response code="404">Monthly log not found</response>
        /// <response code="200">Monthly log record</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<MonthlyLogReadDto>> GetMonthlyLogById(int id)
        {
            MonthlyLogModel domain = await _repository.GetMonthlyLogById(id);
            IEnumerable<CategoryModel> categories = await _categoryRepo.GetCategories();

            if (domain == null || categories == null || categories.Count() <= 0)
                return NotFound();

            string category = categories.Where(cat => domain.CategoryId == cat.Id).Select(cat => cat.Category).FirstOrDefault();

            MonthlyLogReadDto dto = new MonthlyLogReadDto
            {
                Id = domain.Id,
                UserId = domain.UserId,
                Amount = domain.Amount,
                CategoryId = domain.CategoryId,
                Category = category,
                TransactionDate = domain.TransactionDate,
                Comment = domain.Comment,
                Month = domain.Month,
                CreatedDate = domain.CreatedDate,
                ModifiedDate = domain.ModifiedDate,
                ModifiedBy = domain.ModifiedBy,
            };
            return Ok(dto);
        }

        // POST api/monthlyLog
        /// <summary>
        /// Insert MonthlyLog record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Monthly log record</returns>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        /// <response code="201">Monthly log record created</response>
        [HttpPost]
        public async Task<ActionResult> InsertMonthlyLog(MonthlyLogInsertDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            MonthlyLogModel domain = _mapper.Map<MonthlyLogModel>(dto);
            bool isSuccessful = await _repository.InsertMonthlyLog(domain);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);
            MonthlyLogReadDto readDto = _mapper.Map<MonthlyLogReadDto>(domain);
            readDto.Category = dto.Category;
            CreatedAtRouteResult result = CreatedAtRoute(nameof(GetMonthlyLogsByUserId), new { userId = readDto.Id, month = 10 }, readDto);
            return result;
        }

        // PUT api/monthlyLog/{id}
        /// <summary>
        /// Update Monthly log by id
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        /// <response code="400">Bad request</response>
        /// <response code="404">Monthly log not found</response>
        /// <response code="500">Internal server error</response>
        /// <response code="204">Monthly log was successfully updated</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMonthlyLogById(int id, MonthlyLogUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (updateDto.Id == 0)
                updateDto.Id = id;

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

        // DELETE api/monthlyLog/{id}
        /// <summary>
        /// Delete monthly log by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">Monthly log not found</response>
        /// <response code="500">Internal server error</response>
        /// <response code="204">Monthly log was successfully deleted</response>
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

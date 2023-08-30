using AutoMapper;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using DoYouBudget.API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoYouBudget.API.Controllers
{
    /// <summary>
    /// Provides category type data
    /// </summary>
    [Route("api/categoryType")]
    [Produces("application/json")]
    [ApiController]
    public class CategoryTypeController : ControllerBase
    {
        private readonly ICategoryTypeRepo _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Inject Repository and Mapper
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        public CategoryTypeController(ICategoryTypeRepo repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        // GET api/categoryType
        /// <summary>
        /// Get all category types
        /// </summary>
        /// <returns>All category types</returns>
        /// <response code="404">Category type not found</response>
        /// <response code="200">Category types successfully found</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryTypeReadDto>>> GetCategoryTypeAsync()
        {
            IEnumerable<CategoryTypeModel> domain = await _repository.GetCategoryTypeAsync();
            if (domain == null)
                return NotFound();

            IEnumerable<CategoryTypeReadDto> dto = _mapper.Map<IEnumerable<CategoryTypeReadDto>>(domain);
            return Ok(dto);
        }
    }
}

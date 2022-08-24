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
    /// Provides category data
    /// </summary>
    [Route("api/categories")]
    [Produces("application/json")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Injected repo and mapper
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public CategoryController(ICategoryRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET ALL api/categories
        /// <summary>
        /// Get all Categories
        /// </summary>
        /// <returns>All Category records</returns>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetCategories()
        {
            IEnumerable<CategoryModel> domains = await _repository.GetCategories();
            if (domains == null)
                return NotFound();
            IEnumerable<CategoryReadDto> dtos = _mapper.Map<IEnumerable<CategoryReadDto>>(domains);
            return Ok(dtos);
        }

        // GET api/categories/{id}
        /// <summary>
        /// Get Category record by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = nameof(GetCategoryById))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CategoryReadDto>> GetCategoryById(int id)
        {
            CategoryModel domain = await _repository.GetCategoryById(id);
            if (domain == null)
                return NotFound();
            CategoryReadDto dto = _mapper.Map<CategoryReadDto>(domain);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<int>> InsertCategory(CategoryInsertDto insertDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            CategoryModel domain = _mapper.Map<CategoryModel>(insertDto);
            bool isSuccessful = await _repository.InsertCategory(domain);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);
            CategoryReadDto readDto = _mapper.Map<CategoryReadDto>(domain);
            return CreatedAtRoute(nameof(GetCategoryById), new { id = readDto.Id }, readDto);
        }

    }
}
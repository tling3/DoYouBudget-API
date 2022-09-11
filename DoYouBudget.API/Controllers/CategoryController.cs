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
        public ActionResult<List<CategoryReadDto>> GetCategories()
        {
            List<CategoryReadDto> readDtos = _repository.GetCategories();
            if (readDtos == null)
                return NotFound();
            return Ok(readDtos);
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

        // POST api/categories
        /// <summary>
        /// Post category
        /// </summary>
        /// <param name="insertDto"></param>
        /// <returns>category</returns>
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

        /// <summary>
        /// Update category by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        /// <response code="400">Updated item is not valid</response>
        /// <response code="404">Item not found</response>
        /// <response code="500">Item failed to be updated</response>
        /// <response code="204">Item was successfully updated</response>
        // PUT api/categories/
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateCategory(int id, CategoryUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            CategoryModel domain = await _repository.GetCategoryById(id);
            if (domain == null)
                return NotFound();

            _mapper.Map(updateDto, domain);
            _repository.UpdateCategory(domain);
            bool isSuccessful = _repository.SaveChanges();
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }

        /// <summary>
        /// Delete category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="500">Item failed to be updated</response>
        /// <response code="204">Item was successfully updated</response>
        // DELETE api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            CategoryModel domain = await _repository.GetCategoryById(id);
            if (domain == null)
                return NotFound();

            bool isSuccessful = _repository.DeleteCategory(domain);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }
    }
}
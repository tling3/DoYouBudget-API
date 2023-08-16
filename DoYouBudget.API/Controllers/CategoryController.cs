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
    /// Provides category data
    /// </summary>
    [Route("api/categories")]
    [Produces("application/json")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _repository;
        private readonly ICategoryTypeRepo _typeRepo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Injected repo and mapper
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="typeRepo"></param>
        /// <param name="mapper"></param>
        public CategoryController(
            ICategoryRepo repository,
            ICategoryTypeRepo typeRepo,
            IMapper mapper)
        {
            _repository = repository;
            _typeRepo = typeRepo;
            _mapper = mapper;
        }

        // GET ALL api/categories
        /// <summary>
        /// Get all Categories
        /// </summary>
        /// <returns>All Category records</returns>
        /// <response code="404">Categories not found</response>
        /// <response code="200">Categories successfully found</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetCategories()
        {
            IEnumerable<CategoryModel> categoryDomains = await _repository.GetCategories();
            IEnumerable<CategoryTypeModel> typeDomains = await _typeRepo.GetCategoryType();

            if (categoryDomains == null || typeDomains == null)
                return NotFound();

            List<CategoryReadDto> categoryDtos = categoryDomains
                .Join(
                    typeDomains,
                    category => category.TypeId,
                    type => type.Id,
                    (category, type) => new CategoryReadDto()
                    {
                        Id = category.Id,
                        Type = type.Type,
                        UserId = category.UserId,
                        Category = category.Category,
                        Budget = category.Budget,
                        TypeId = category.TypeId,
                        PostDate = category.PostDate,
                        ModifiedBy = category.ModifiedBy,
                        CreatedDate = category.CreatedDate,
                        ModifiedDate = category.ModifiedDate
                    }
                ).ToList();

            return Ok(categoryDtos);
        }

        // GET api/categories/{id}
        /// <summary>
        /// Get Category record by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category record</returns>
        /// <response code="404">Not found</response>
        /// <response code="200">Category record</response>
        [HttpGet("{id}", Name = nameof(GetCategoryById))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CategoryReadDto>> GetCategoryById(int id)
        {
            CategoryModel domain = await _repository.GetCategoryById(id);
            IEnumerable<CategoryTypeModel> typeDomain = await _typeRepo.GetCategoryType();
            if (domain == null || typeDomain == null)
                return NotFound();
            CategoryReadDto dto = _mapper.Map<CategoryReadDto>(domain);
            dto.Type = typeDomain.Where(type => type.Id == dto.TypeId).Select(type => type.Type).FirstOrDefault();
            return Ok(dto);
        }

        // POST api/categories
        /// <summary>
        /// Post category
        /// </summary>
        /// <param name="insertDto"></param>
        /// <returns>Category record</returns>
        /// <response code="500">Server error</response>
        /// <response code="201">Category record created</response>
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

        // PUT api/categories/
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
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateCategory(int id, CategoryUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (updateDto.Id == 0)
                updateDto.Id = id;

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

        // DELETE api/categories/{id}
        /// <summary>
        /// Delete category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="500">Item failed to be deleted</response>
        /// <response code="204">Item was deleted</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            CategoryModel domain = await _repository.GetCategoryById(id);
            if (domain == null)
                return NotFound();

            domain.Deleted = true;
            _repository.UpdateCategory(domain);
            bool isSuccessful = _repository.SaveChanges();
            //bool isSuccessful = _repository.DeleteCategory(domain);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }
    }
}

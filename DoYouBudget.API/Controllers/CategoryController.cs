using AutoMapper;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using DoYouBudget.API.Models.Dto;
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
        /// Get all categories
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



    }
}
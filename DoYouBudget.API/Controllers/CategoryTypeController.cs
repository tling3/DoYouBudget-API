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
    /// 
    /// </summary>
    [Route("api/categoryType")]
    [Produces("application/json")]
    [ApiController]
    public class CategoryTypeController : ControllerBase
    {
        private readonly ICategoryTypeRepo _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inject Repository and Mapper
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public CategoryTypeController(ICategoryTypeRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Category Types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<CategoryTypeReadDto>>> GetCategoryType()
        {
            IEnumerable<CategoryTypeModel> domain = await _repository.GetCategoryType();
            if (domain == null)
                return NotFound();

            IEnumerable<CategoryTypeReadDto> dto = _mapper.Map<IEnumerable<CategoryTypeReadDto>>(domain);
            return Ok(dto);
        }
    }
}

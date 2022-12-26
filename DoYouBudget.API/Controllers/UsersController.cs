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
    /// Provides users data
    /// </summary>
    [Route("api/users")]
    [Produces("application/json")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepo _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Injected repo and mapper
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public UsersController(IUsersRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET ALL api/users
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>All User records</returns>
        /// <response code="404">Users not found</response>
        /// <response code="200">Users successfully found</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersReadDto>>> GetUsers()
        {
            IEnumerable<UsersModel> domains = await _repository.GetUsers();
            if (domains == null)
                return NotFound();
            IEnumerable<UsersReadDto> dtos = _mapper.Map<IEnumerable<UsersReadDto>>(domains);
            return Ok(dtos);
        }
    }
}




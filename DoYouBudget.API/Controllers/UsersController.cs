using AutoMapper;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using DoYouBudget.API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoYouBudget.API.Controllers
{
    [Route("api/users")]
    [Produces("application/json")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepo _repository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET ALL api/attendance
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>All user records</returns>
        /// <response code="404">Item(s) not found</response>
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




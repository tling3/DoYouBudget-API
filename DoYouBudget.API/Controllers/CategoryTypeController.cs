using AutoMapper;
using DoYouBudget.API.Data.Interfaces;
using DoYouBudget.API.Models.Domain;
using DoYouBudget.API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
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
        public async Task<ActionResult<IEnumerable<CategoryTypeReadDto>>> GetCategoryType()
        {
            IEnumerable<CategoryTypeModel> domain = await _repository.GetCategoryType();
            if (domain == null)
                return NotFound();

            IEnumerable<CategoryTypeReadDto> dto = _mapper.Map<IEnumerable<CategoryTypeReadDto>>(domain);
            return Ok(dto);
        }

        // STORED PROCEDURE EXAMPLE - SAVE FOR STUDY PURPOSES

        /// <summary>
        /// Get Category Types
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult<List<CategoryTypeReadDto>>> GetCategoryType()
        //{
        //    //IEnumerable<CategoryTypeModel> domain = await _repository.GetCategoryType();

        //    SqlConnection conn = new SqlConnection();
        //    SqlCommand cmd = new SqlCommand();

        //    string connectionString = _configuration.GetConnectionString("DoYouBudgetConnection");

        //    conn.ConnectionString = connectionString;
        //    cmd.Connection = conn;
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "CATEGORY_TYPE_GET";

        //    List<CategoryTypeModel> domain = new List<CategoryTypeModel>();

        //    conn.Open();
        //    using (conn)
        //    {
        //        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        //        while (reader.Read())
        //        {
        //            CategoryTypeModel model = new CategoryTypeModel();
        //            int index = 0;

        //            model.Id = reader.GetInt32(index++);
        //            model.Type = reader.GetString(index++);
        //            model.CreatedDate = reader.GetDateTime(index++);
        //            model.ModifiedDate = reader.GetDateTime(index++);
        //            model.ModifiedBy = reader.GetString(index++);
        //            domain.Add(model);
        //        }
        //    }
        //    conn.Close();
        //    conn.Dispose();

        //    if (domain == null)
        //        return NotFound();

        //    IEnumerable<CategoryTypeReadDto> dto = _mapper.Map<List<CategoryTypeReadDto>>(domain);
        //    return Ok(dto);
        //}


        // STORED PROCEDURE EXAMPLE - SAVE FOR STUDY PURPOSES

        /// <summary>
        /// Insert category type record
        /// </summary>
        /// <param name="categoryTypeReadDto"></param>
        /// <returns>Category type record</returns>
        /// <response></response>
        //[HttpPost]
        //public async Task<IActionResult> InsertCategoryType(CategoryTypeReadDto categoryTypeReadDto)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            SqlConnection conn = new SqlConnection();
        //            SqlCommand cmd = new SqlCommand();

        //            string connectionString = _configuration.GetConnectionString("DoYouBudgetConnection");
        //            conn.ConnectionString = connectionString;
        //            cmd.Connection = conn;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "CATEGORY_TYPE_INSERT";

        //            SqlParameter parms = new SqlParameter();
        //            parms.ParameterName = "@Id";
        //            parms.SqlDbType = SqlDbType.Int;
        //            parms.Direction = ParameterDirection.Output;

        //            cmd.Parameters.Add(parms);
        //            cmd.Parameters.AddWithValue("@Type", categoryTypeReadDto.Type);
        //            cmd.Parameters.AddWithValue("@CreatedDate", categoryTypeReadDto.CreatedDate);
        //            cmd.Parameters.AddWithValue("@ModifiedDate", categoryTypeReadDto.ModifiedDate);
        //            cmd.Parameters.AddWithValue("@ModifiedBy", categoryTypeReadDto.ModifiedBy);

        //            conn.Open();
        //            using (conn)
        //                cmd.ExecuteNonQuery();
        //            conn.Close();
        //            conn.Dispose();

        //            int insertId = (int)cmd.Parameters["@Id"].Value;

        //            return NoContent();
        //        }
        //        else
        //            return BadRequest(ModelState);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}

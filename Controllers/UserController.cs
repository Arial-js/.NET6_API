using Dotnet6_API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dotnet6_API.Exceptions;
using AutoMapper;
using Dotnet6_API.Models.User;
using Dotnet6_API.Models.DTO.User;

namespace Dotnet6_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IServiceLayer _serviceLayer;
        private readonly IMapper _mapper;
        public UserController(IServiceLayer serviceLayer, IMapper mapper)
        {
            _serviceLayer = serviceLayer;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<UsersModel>> GetAllUsers()
        {
            return await _serviceLayer.ListAllAsync();
        }

        [HttpGet("GetById")]
        public async Task<UsersModel> GetUserById(int id)
        {
            return await _serviceLayer.GetByIdAsync(id);
        }

        [HttpGet("GetByEmail")]
        public async Task<UsersModel> GetUserByEmail(string email)
        {
            return await _serviceLayer.GetByEmailAsync(email);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateUser([FromBody] UserWithoutRoleAndIdDTO user)
        {
            try
            {
                var mapUser = _mapper.Map<UsersModel>(user);
                var newUser = await _serviceLayer.CreateAsync(mapUser);
                return Created("test", newUser);
            }
            catch(DuplicateEmailException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateWithRole")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateUserWithRole([FromBody] UserWithoutIdDTO user)
        {
            try
            {
                var mapUser = _mapper.Map<UsersModel>(user);
                var newUser = await _serviceLayer.CreateAsAdminAsync(mapUser);
                return Created("test", newUser);
            }
            catch(DuplicateEmailException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserWithoutIdDTO user)
        {
            try
            {
                var mapUser = _mapper.Map<UsersModel>(user);
                mapUser.Id = id;
                var updatedUser = await _serviceLayer.UpdateAsync(id, mapUser);
                return Ok(updatedUser);
            }
            catch(BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _serviceLayer.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

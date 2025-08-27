using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.IUserDataRepository;

namespace MyTasks.API
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly IUserDataRepository _repository;

        public UserDataController(IUserDataRepository repository)
        {
            _repository = repository;
        }

        [HttpPatch("{userId:Guid}")]
        public async Task<IActionResult> PatchUser(Guid? userId, [FromBody] UserWithLoginDto data)
        {
            if (userId == null)
                return BadRequest("No User ID provided.");

            if (data == null)
                return BadRequest("Invalid data.");

            try
            {
                await _repository.UpdateUserData(userId, data);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> AddUser([FromBody] UserWithLoginDto data)
        {
            if (data == null)
                return BadRequest("Invalid data.");

            try
            {
                await _repository.AddUserData(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("hard/{userId:Guid}")]
        public async Task<IActionResult> HardDeleteUser(Guid? userId)
        {
            if (userId == null)
                return BadRequest("No User ID provided.");

            try
            {
                await _repository.HardDeleteUserData(userId);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("soft/{userId:Guid}")]
        public async Task<IActionResult> SoftDeleteUser(Guid? userId)
        {
            if (userId == null)
                return BadRequest("No User ID provided.");

            try
            {
                await _repository.SoftDeleteUserData(userId);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"An error occurred: {ex.Message}");
            }
        }
    }
}
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
    public class UsersController : ControllerBase
    {
        private readonly IUserDataRepository _repository;

        public UsersController(IUserDataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.GetAllUserData();
            return Ok(users);
        }

        // GET /api/users/{id}
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var user = await _repository.GetUserData(userId);
            if (user == null)
                return NotFound($"User with id {userId} not found.");

            return Ok(user);
        }

        // POST /api/users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserWithLoginDto data)
        {
            if (data == null)
                return BadRequest("Invalid data.");

            try
            {
                var newUser = await _repository.AddUserData(data);
                return CreatedAtAction(nameof(Get), new { userId = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"An error occurred: {ex.Message}");
            }
        }

        // PUT /api/users/{id}
        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> Update(Guid userId, [FromBody] UserWithLoginDto data)
        {
            if (data == null)
                return BadRequest("Invalid data.");

            try
            {
                await _repository.UpdateUserData(userId, data);
                return NoContent();
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

        // PATCH /api/users/{id}/deactivate (soft delete)
        [HttpPatch("{userId:guid}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid userId)
        {
            try
            {
                await _repository.SoftDeleteUserData(userId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"An error occurred: {ex.Message}");
            }
        }

        // DELETE /api/users/{id} (hard delete)
        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            try
            {
                await _repository.HardDeleteUserData(userId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"An error occurred: {ex.Message}");
            }
        }
    }
}
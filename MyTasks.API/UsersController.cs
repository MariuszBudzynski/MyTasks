using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.API.Services.Interfaces;
using MyTasks.Repositories.DTOS;

namespace MyTasks.API
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _userService.GetAllUsersAsync());

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserWithLoginDto data)
        {
            if (data == null) return BadRequest();
            var newUser = await _userService.CreateUserAsync(data);
            return CreatedAtAction(nameof(Get), new { userId = newUser.Id }, newUser);
        }

        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> Update(Guid userId, [FromBody] UserWithLoginDto data)
        {
            if (data == null) return BadRequest();
            await _userService.UpdateUserAsync(userId, data);
            return NoContent();
        }

        [HttpPatch("{userId:guid}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid userId)
        {
            await _userService.DeactivateUserAsync(userId);
            return NoContent();
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            await _userService.DeleteUserAsync(userId);
            return NoContent();
        }
    }
}
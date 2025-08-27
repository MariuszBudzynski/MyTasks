using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.IUserDataRepository;

namespace MyTasks.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly IUserDataRepository _repository;
        public UserDataController(IUserDataRepository repository)
        {
            _repository = repository;
        }

        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> PatchUser(Guid? userId, [FromBody] UserWithLoginDto data)
        {
            //impelement later
            await Task.CompletedTask;
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> AddUser([FromBody] UserWithLoginDto data)
        {
            if (data == null)
            {
                return BadRequest("Invalid data.");
            }

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
    }
}

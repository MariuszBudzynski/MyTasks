using Microsoft.AspNetCore.Mvc;
using MyTasks.Repositories.DTOS;

namespace MyTasks.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDataController : ControllerBase
    {
        [HttpPatch("{id:Guid}")]
        public async Task<IActionResult> PatchUser(Guid? id, [FromBody] UserWithLoginDto data)
        {
            //impelement later
            await Task.CompletedTask;
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> AddUser([FromBody] UserWithLoginDto data)
        {
            //impelement later
            await Task.CompletedTask;
            return Ok();
        }
    }
}

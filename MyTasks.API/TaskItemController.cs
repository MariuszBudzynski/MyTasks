using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTasks.API.Services.Interfaces;
using MyTasks.Repositories.DTOS;

namespace MyTasks.API
{
    [Authorize(Roles = "Admin,Regular")]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService _taskItemService;

        public TaskItemController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var taskItem = await _taskItemService.GetByIdAsync(id);
            if (taskItem == null)
                return NotFound();

            return Ok(taskItem);
        }

        [HttpPut("{taskItemId:guid}")]
        public async Task<IActionResult> Update(Guid taskItemId, [FromBody] UpdateTaskItemDto data)
        {
            if (data == null) return BadRequest();

            var taskItems = await _taskItemService.GetTaskItemById(taskItemId);
            if (taskItems == null) return NotFound();

            await _taskItemService.UpdateTaskItemtAsync(data, taskItems, taskItemId);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskItemDto data)
        {
            if (data == null)
                return BadRequest("Invalid data.");

            var userIdClaim = User.FindFirst("userId")?.Value;
            if (userIdClaim == null)
                return Forbid();

            if (!Guid.TryParse(userIdClaim, out var ownerId))
                return BadRequest("Invalid user id in token.");

            try
            {
                var taskItemResponse = await _taskItemService.CreateAsync(data, ownerId);

                return CreatedAtAction(nameof(GetById), new { id = taskItemResponse.Id }, taskItemResponse);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}
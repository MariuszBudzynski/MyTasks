using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.ITaskItemRepository;

namespace MyTasks.API
{
    [Authorize(Roles = "Admin,Regular")]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        //move buisnes logic to servis later
        private readonly ITaskItemRepository _taskItemRepository;
        public TaskItemController(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _taskItemRepository.GetTaskItemtByIdAsync(id);
            if (project == null)
                return NotFound();

            return Ok(project);
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
                var taskItemId = Guid.NewGuid();

                var taskItem = new TaskItemModel
                {
                    Id = taskItemId,
                    Title = data.Title,
                    Description = data.Description,
                    DueDate = data.DueDate,
                    IsCompleted = data.IsCompleted,
                    ProjectId = data.ProjectId,
                    AssignedUserId = ownerId
                };

                await _taskItemRepository.AddTaskItemAsync(taskItem);

                var taskItemResponse = new TaskItemResponseDto(
                    taskItemId,
                    taskItem.Title,
                    taskItem.Description,
                    taskItem.DueDate,
                    taskItem.IsCompleted,
                    taskItem.ProjectId,
                    taskItem.AssignedUserId
                    );



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

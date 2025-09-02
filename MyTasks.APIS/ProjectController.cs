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
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto data)
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
                var projectResponse = await _projectService.CreateProjectAsync(data, ownerId);
                return CreatedAtAction(nameof(GetById), new { id = projectResponse.Id }, projectResponse);
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
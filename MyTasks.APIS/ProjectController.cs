using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.IProjecRepository;

namespace MyTasks.API
{
    [Authorize(Roles = "Admin,Regular")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjecRepository _projecRepository;
        private readonly IUserOperationsRepository _userRepository;

        public ProjectController(
            IProjecRepository projecRepository,
            IUserOperationsRepository userRepository)
        {
            _projecRepository = projecRepository;
            _userRepository = userRepository;
        }

        // GET /api/project/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _projecRepository.GetById(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        // POST /api/project
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto data)
        {
            if (data == null)
                return BadRequest("Invalid data.");

            var userIdClaim = User.FindFirst("userId")?.Value;
            if (userIdClaim == null)
            {
                return Forbid();
            }

            if (!Guid.TryParse(userIdClaim, out var ownerId))
                return BadRequest("Invalid user id in token.");

            var user = await _userRepository.GetUserByIdAsync(ownerId);

            if (user == null)
                return BadRequest("User does not exist.");

            try
            {
                var project = new ProjectModel
                {
                    Id = Guid.NewGuid(),
                    Name = data.Name,
                    Description = data.Description,
                    OwnerId = ownerId,
                    Owner = user,
                };

                await _projecRepository.AddProject(project);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = project.Id },
                    new ProjectResponseDto(
                        project.Id,
                        project.Name,
                        project.Description,
                        project.OwnerId ?? Guid.Empty)
                );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"An error occurred: {ex.Message}");
            }
        }
    }
}

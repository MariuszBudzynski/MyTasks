using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTasks.DbOperations.Repositories;
using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.IProjecRepository;

namespace MyTasks.API
{
    [Authorize(Roles = "Admin,Regular")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjecController : ControllerBase
    {
        private readonly IProjecRepository _projecRepository;
        public ProjecController(IProjecRepository projecRepository)
        {
            _projecRepository = projecRepository;
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

            try
            {
                var project = new ProjectModel
                {
                    Id = Guid.NewGuid(),
                    Name = data.Name,
                    Description = data.Description,
                    OwnerId = new Guid(userIdClaim),
                };

                await _projecRepository.AddProject(project);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = project.Id },
                    project
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

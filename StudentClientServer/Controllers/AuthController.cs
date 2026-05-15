using Microsoft.AspNetCore.Mvc;
using StudentTrackerLib.DTOs.DTOAuth;
using StudentTrackerLib.DTOs;
using StudentTrackerLib.Models;
using StudentTrackerServer.Services;
using StudentTrackerLib.Exceptions;
using StudentTrackerLib.DTOs.DTOTeacher;

namespace StudentTrackerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TeachersDbCollectionService _service;

        public AuthController(TeachersDbCollectionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<TeacherResponse>> Authenticate([FromBody] AuthRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                var teacher = new Teacher()
                {
                    Name = request.Name,
                    PasswordHash = request.PasswordHash,
                };
                teacher = await _service.Authenticate(teacher, cancellationToken);
                return Ok(teacher?.ToDto());
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using StudentTrackerServer.Services;
using StudentTrackerLib.Models;
using StudentTrackerLib.DTOs.DTOTeacher;
using StudentTrackerLib.DTOs;

namespace StudentTrackerServer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TeachersController : ControllerBase
	{
		private readonly TeachersDbCollectionService _service;

		public TeachersController(TeachersDbCollectionService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TeacherResponse>>> GetAll(CancellationToken cancellationToken)
		{
			return Ok((await _service.GetAllAsync(cancellationToken)).Select(x => x.ToDto()).ToList());
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<TeacherResponse>> Get(int id, CancellationToken cancellationToken)
		{
			var result = await _service.GetAsync(id, cancellationToken);
			if (result == null)
				return NotFound(result);
			return Ok(result.ToDto());
		}
		[HttpPost]
		public async Task<ActionResult<TeacherResponse>> Create([FromBody] CreateTeacherDto teacher,CancellationToken cancellationToken)
		{
			try
			{
				var item = new Teacher()
				{
					Name = teacher.Name,
					PasswordHash = teacher.PasswordHash,
				};
				var result = await _service.AddAsync(item, cancellationToken);
				return Ok(result?.ToDto());
			}
			catch
			{
				return BadRequest(teacher);
			}
		}
		[HttpPut("{id}")]
		public async Task<ActionResult<TeacherResponse>> Update(int id, [FromBody] UpdateTeacherDto teacher, CancellationToken cancellationToken)
		{
			try
			{
				var item = new Teacher()
				{
					Name = teacher.Name,
					PasswordHash = teacher.PasswordHash,
				};
				var result = await _service.EditAsync(id, item, cancellationToken);
				return Ok(result?.ToDto());
			}
			catch (ArgumentException)
			{
				return NotFound(teacher);
			}
			catch
			{
				return BadRequest(teacher);
			}
		}
		[HttpPut("{teacherId}-{subjectId}")]
		public async Task<ActionResult<TeacherResponse>> AssignSubject(int teacherId, int subjectId, CancellationToken cancellationToken)
		{
            try
            {
                var result = await _service.AssignSubjectAsync(teacherId, subjectId, cancellationToken);
                return Ok(result?.ToDto());
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
        [HttpDelete("{teacherId}-{subjectId}")]
        public async Task<ActionResult<TeacherResponse>> DeassignSubject(int teacherId, int subjectId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.DeassignSubjectAsync(teacherId, subjectId, cancellationToken);
                return Ok(result?.ToDto());
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
        [HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
		{

			try
			{
				await _service.RemoveAsync(id, cancellationToken);
				return Ok();
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

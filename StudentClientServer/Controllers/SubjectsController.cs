using Microsoft.AspNetCore.Mvc;
using StudentTrackerLib.DTOs;
using StudentTrackerLib.DTOs.DTOSubject;
using StudentTrackerLib.Models;
using StudentTrackerServer.Services;

namespace StudentTrackerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly SubjectsDbCollectionService _service;

        public SubjectsController(SubjectsDbCollectionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectResponse>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok((await _service.GetAllAsync(cancellationToken)).Select(x => x.ToDto()).ToList());
        }
        [HttpGet("teacherId:{teacherId}")]
        public async Task<ActionResult<IEnumerable<SubjectResponse>>> GetByTeacherId(int teacherId,CancellationToken cancellationToken)
        {
            return Ok((await _service.GetByTeacherIdAsync(teacherId, cancellationToken))
                .Select(x => x.ToDto()).ToList());
        }
        [HttpGet("id:{id}")]
        public async Task<ActionResult<SubjectResponse>> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetAsync(id, cancellationToken);
            if (result == null)
                return NotFound(result);
            return Ok(result.ToDto());
        }
        [HttpPost]
        public async Task<ActionResult<SubjectResponse>> Create([FromBody] CreateSubjectDto subject, CancellationToken cancellationToken)
        {
            try
            {
                var item = new Subject()
                {
                    Name = subject.Name
                };
                var result = await _service.AddAsync(item, cancellationToken);
                return Ok(result?.ToDto());
            }
            catch
            {
                return BadRequest(subject);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<SubjectResponse>> Update(int id, [FromBody] UpdateSubjectDto subject, CancellationToken cancellationToken)
        {
            try
            {
                var item = new Subject()
                {
                    Name = subject.Name,
                };
                var result = await _service.EditAsync(id, item, cancellationToken);
                return Ok(result?.ToDto());
            }
            catch (ArgumentException)
            {
                return NotFound(subject);
            }
            catch
            {
                return BadRequest(subject);
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

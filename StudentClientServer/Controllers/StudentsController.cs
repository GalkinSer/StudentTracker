using Microsoft.AspNetCore.Mvc;
using StudentTrackerLib.DTOs;
using StudentTrackerLib.DTOs.DTOStudent;
using StudentTrackerLib.Models;
using StudentTrackerServer.Services;


namespace StudentTrackerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsDbCollectionService _service;

        public StudentsController(StudentsDbCollectionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentResponse>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok((await _service.GetAllAsync(cancellationToken)).Select(x => x.ToDto()).ToList());
        }
        [HttpGet("groupId:{groupId}")]
        public async Task<ActionResult<IEnumerable<StudentResponse>>> GetByGroupId(int groupId, CancellationToken cancellationToken)
        {
            return Ok((await _service.GetByGroupIdAsync(groupId, cancellationToken))
                .Select(x => x.ToDto()).ToList());
        }
        [HttpGet("id:{id}")]
        public async Task<ActionResult<StudentResponse>> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetAsync(id, cancellationToken);
            if (result == null)
                return NotFound(result);
            return Ok(result?.ToDto());
        }
        [HttpPost]
        public async Task<ActionResult<StudentResponse>> Create([FromBody] CreateStudentDto student, CancellationToken cancellationToken)
        {
            try
            {
                var item = new Student()
                {
                    Name = student.Name,
                    StudentCardNumber = student.StudentCardNumber,
                    GroupId = student.GroupId,
                    IsRepresentative = student.IsRepresentative,
                };
                var result = await _service.AddAsync(item, cancellationToken);
                return Ok(result?.ToDto());
            }
            catch
            {
                return BadRequest(student);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentResponse>> Update(int id, [FromBody] UpdateStudentDto student, CancellationToken cancellationToken)
        {
            try
            {
                var item = new Student()
                {
                    Name = student.Name,
                    GroupId = student.GroupId,
                    IsRepresentative = student.IsRepresentative,
                };
                var result = await _service.EditAsync(id, item, cancellationToken);
                return Ok(result?.ToDto());
            }
            catch (ArgumentException)
            {
                return NotFound(student);
            }
            catch
            {
                return BadRequest(student);
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

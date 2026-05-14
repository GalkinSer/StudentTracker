using Microsoft.AspNetCore.Mvc;
using StudentTrackerLib.DTOs;
using StudentTrackerLib.DTOs.DTOGroup;
using StudentTrackerLib.DTOs.DTOTeacher;
using StudentTrackerLib.Models;
using StudentTrackerServer.Services;

namespace StudentTrackerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly GroupsDbCollectionService _service;

        public GroupsController(GroupsDbCollectionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupResponse>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok((await _service.GetAllAsync(cancellationToken)).Select(x => x.ToDto()).ToList());
        }
        [HttpGet("subjectId:{subjectId}")]
        public async Task<ActionResult<IEnumerable<GroupResponse>>> GetBySubjectId(int subjectId, CancellationToken cancellationToken)
        {
            return Ok((await _service.GetBySubjectIdAsync(subjectId, cancellationToken))
                .Select(x => x.ToDto()).ToList());
        }
        [HttpGet("id:{id}")]
        public async Task<ActionResult<GroupResponse>> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetAsync(id, cancellationToken);
            if (result == null)
                return NotFound(result);
            return Ok(result.ToDto());
        }
        [HttpPost]
        public async Task<ActionResult<GroupResponse>> Create([FromBody] CreateGroupDto group, CancellationToken cancellationToken)
        {
            try
            {
                var item = new Group() { Name = group.Name };
                var result = await _service.AddAsync(item, cancellationToken);
                return Ok(result?.ToDto());
            }
            catch
            {
                return BadRequest(group);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<GroupResponse>> Update(int id, [FromBody] UpdateGroupDto group, CancellationToken cancellationToken)
        {
            try
            {
                var item = new Group()
                {
                    Name = group.Name,
                };
                var result = await _service.EditAsync(id, item, cancellationToken);
                return Ok(result?.ToDto());
            }
            catch (ArgumentException)
            {
                return NotFound(group);
            }
            catch
            {
                return BadRequest(group);
            }
        }
        [HttpPut("{groupId}-{subjectId}")]
        public async Task<ActionResult<GroupResponse>> AssignSubject(int groupId, int subjectId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.AssignSubjectAsync(groupId, subjectId, cancellationToken);
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
        [HttpDelete("{groupId}-{subjectId}")]
        public async Task<ActionResult<TeacherResponse>> DeassignSubject(int groupId, int subjectId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.DeassignSubjectAsync(groupId, subjectId, cancellationToken);
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

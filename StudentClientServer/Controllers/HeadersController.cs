using Microsoft.AspNetCore.Mvc;
using StudentTrackerLib.DTOs;
using StudentTrackerLib.DTOs.DTOHeader;
using StudentTrackerLib.Models;
using StudentTrackerServer.Services;

namespace StudentTrackerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeadersController : ControllerBase
    {
        private readonly HeadersDbCollectionService _service;

        public HeadersController(HeadersDbCollectionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeaderResponse>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok((await _service.GetAllAsync(cancellationToken)).Select(x => x.ToDto()).ToList());
        }
        [HttpGet("ids:{teacherId}-{subjectId}-{groupId}")]
        public async Task<ActionResult<IEnumerable<HeaderResponse>>> GetByIds(int teacherId, int subjectId, int groupId, CancellationToken cancellationToken)
        {
            return Ok((await _service.GetByIdsAsync(teacherId, subjectId, groupId, cancellationToken))
                .Select(x => x.ToDto()).ToList());
        }
        [HttpGet("id:{id}")]
        public async Task<ActionResult<HeaderResponse>> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetAsync(id, cancellationToken);
            if (result == null)
                return NotFound(result);
            return Ok(result?.ToDto());
        }
        [HttpPost]
        public async Task<ActionResult<HeaderResponse>> Create([FromBody] CreateHeaderDto header, CancellationToken cancellationToken)
        {
            try
            {
                var item = new Header()
                {
                    Title = header.Title,
                    SubjectId = header.SubjectId,
                    GroupId = header.GroupId,
                    TeacherId = header.TeacherId,
                };
                var result = await _service.AddAsync(item, cancellationToken);
                return Ok(result?.ToDto());
            }
            catch
            {
                return BadRequest(header);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<HeaderResponse>> Update(int id, [FromBody] UpdateHeaderDto header, CancellationToken cancellationToken)
        {
            try
            {
                var item = new Header()
                {
                    Id = id,
                    Title = header.Title,
                };
                var result = await _service.EditAsync(id, item, cancellationToken);
                return Ok(result?.ToDto());
            }
            catch (ArgumentException)
            {
                return NotFound(header);
            }
            catch
            {
                return BadRequest(header);
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

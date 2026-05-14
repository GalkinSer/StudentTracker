using Microsoft.AspNetCore.Mvc;
using StudentTrackerLib.DTOs;
using StudentTrackerLib.DTOs.DTOMark;
using StudentTrackerLib.Models;
using StudentTrackerServer.Services;

namespace StudentTrackerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarksController : ControllerBase
    {
        private readonly MarksDbCollectionService _service;

        public MarksController(MarksDbCollectionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarkResponse>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok((await _service.GetAllAsync(cancellationToken)).Select(x => x.ToDto()).ToList());
        }
        [HttpGet("headerId:{headerId}")]
        public async Task<ActionResult<IEnumerable<MarkResponse>>> GetByHeaderId(int headerId, CancellationToken cancellationToken)
        {
            return Ok((await _service.GetByHeaderIdAsync(headerId, cancellationToken))
                .Select(x => x.ToDto()).ToList());
        }
        [HttpGet("id:{id}")]
        public async Task<ActionResult<MarkResponse>> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetAsync(id, cancellationToken);
            if (result == null)
                return NotFound(result);
            return Ok(result.ToDto());
        }
        [HttpPost]
        public async Task<ActionResult<MarkResponse>> Create([FromBody] CreateMarkDto mark, CancellationToken cancellationToken)
        {
            try
            {
                var item = new Mark()
                {
                    Content = mark.Content,
                    HeaderId = mark.HeaderId,
                    StudentId = mark.StudentId,
                };
                var result = await _service.AddAsync(item, cancellationToken);
                return Ok(result?.ToDto());
            }
            catch
            {
                return BadRequest(mark);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<MarkResponse>> Update(int id, [FromBody] UpdateMarkDto mark, CancellationToken cancellationToken)
        {
            try
            {
                var item = new Mark()
                {
                    Content = mark.Content,
                };
                var result = await _service.EditAsync(id, item, cancellationToken);
                return Ok(result?.ToDto());
            }
            catch (ArgumentException)
            {
                return NotFound(mark);
            }
            catch
            {
                return BadRequest(mark);
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

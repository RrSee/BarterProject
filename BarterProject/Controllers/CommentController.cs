using BarterProject.Application.CQRS.Comments.Commands.Requests;
using BarterProject.Application.CQRS.Comments.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BarterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ISender _sender;

        public CommentController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequest request)
        {
            var result = await _sender.Send(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentRequest request)
        {
            request.Id = id;
            var result = await _sender.Send(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int deletedBy)
        {
            var result = await _sender.Send(new DeleteCommentRequest { Id = id, DeletedBy = deletedBy });
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sender.Send(new GetByIdCommentRequest { Id = id });
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sender.Send(new GetAllCommentRequest());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("item/{itemId}")]
        public async Task<IActionResult> GetByItemId(int itemId)
        {
            var result = await _sender.Send(new GetByItemIdCommentRequest { ItemId = itemId });
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}

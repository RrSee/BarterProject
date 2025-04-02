using BarterProject.Application.CQRS.BarterRequests.Commands.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BarterProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BarterRequestController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBarterRequestRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, int deletedBy)
    {
        var result = await _sender.Send(new DeleteBarterRequestRequest { Id = id, DeletedBy = deletedBy });
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}

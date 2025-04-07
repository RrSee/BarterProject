using System.Reflection;
using BarterProject.Application.CQRS.Users.Commads.Requests;
using BarterProject.Application.CQRS.Users.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarterProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("Id")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetByIdUserRequest { UserId = id});
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    [HttpGet("Email")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await _sender.Send(new GetByEmailUserRequest { Email = email });
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new GetAllUserRequest());
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id, [FromQuery] int deletedBy)
    {
        var result = await _sender.Send(new DeleteUserRequest { Id = id, DeletedBy = deletedBy });
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request) { 
        var result = await _sender.Send(request);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        return Ok(await _sender.Send(request));
    }
}

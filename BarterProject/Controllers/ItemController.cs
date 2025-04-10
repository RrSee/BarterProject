﻿using BarterProject.Application.CQRS.Items.Commands.Requests;
using BarterProject.Application.CQRS.Items.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BarterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ISender _sender;

        public ItemController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddItemCommandRequest request)
        {
            var result = await _sender.Send(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateItemCommandRequest request)
        {
            request.Id = id;
            var result = await _sender.Send(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int deletedBy)
        {
            var result = await _sender.Send(new DeleteItemCommandRequest { Id = id, DeletedBy = deletedBy });
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sender.Send(new GetByIdItemQueryRequest { Id = id });
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sender.Send(new GetAllItemsQueryRequest());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var result = await _sender.Send(new GetByUserIdItemsQueryRequest { UserId = userId });
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchItemsByName([FromQuery] string keyword)
        {
            var result = await _sender.Send(new SearchItemsByNameQueryRequest { Keyword = keyword });
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Errors);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var result = await _sender.Send(new GetItemsByCategoryIdQueryRequest(categoryId));
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Errors);
        }

        [HttpGet("searchByCategoryAndName")]
        public async Task<IActionResult> SearchItemsByCategoryAndName([FromQuery] int? categoryId, [FromQuery] string keyword)
        {
            var result = await _sender.Send(new SearchItemsByCategoryAndNameQueryRequest(categoryId, keyword));
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Errors);
        }
    }
}
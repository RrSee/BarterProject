using BarterProject.Application.CQRS.Categories.Command.Requests;
using BarterProject.Application.CQRS.Categories.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ISender _sender;

        public CategoryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommandRequest request)
        {
            var result = await _sender.Send(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryCommandRequest request)
        {
            request.Id = id;
            var result = await _sender.Send(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int deletedBy)
        {
            var result = await _sender.Send(new DeleteCategoryCommandRequest { Id = id, DeletedBy = deletedBy });
            return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Parametreli yapıcı kullanarak id'yi geçiyoruz
            var result = await _sender.Send(new GetCategoryByIdQueryRequest(id));

            return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
        }

        // Tüm kategorileri listeleme
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sender.Send(new GetAllCategoriesQueryRequest());
            return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
        }
    }
}

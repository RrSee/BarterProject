using AutoMapper;
using BarterProject.Application.CQRS.Items.Queries.Requests;
using BarterProject.Application.CQRS.Items.Queries.Responses;
using BarterProject.Common.Exceptions;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Handlers.QueryHandlers;

public class GetByUserIdItemsQueryHandler : IRequestHandler<GetByUserIdItemsQueryRequest, Result<List<GetByUserIdItemsQueryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByUserIdItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetByUserIdItemsQueryResponse>>> Handle(GetByUserIdItemsQueryRequest request, CancellationToken cancellationToken)
    {
        var items = await _unitOfWork.ItemRepository.GetByUserIdAsync(request.UserId);

        if (items == null || items.Count()== 0)
        {
            //return new Result<List<GetByUserIdItemsQueryResponse>>(new List<string> { "No items found for this user" });
            throw new BadRequestException("No items found for this user");
        }

        var response = _mapper.Map<List<GetByUserIdItemsQueryResponse>>(items);
        return new Result<List<GetByUserIdItemsQueryResponse>> { Data = response };
    }
}
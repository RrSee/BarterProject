using AutoMapper;
using BarterProject.Application.CQRS.Items.Queries.Requests;
using BarterProject.Application.CQRS.Items.Queries.Responses;
using BarterProject.Common.Exceptions;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Handlers.QueryHandlers;

public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQueryRequest, Result<List<GetAllItemsQueryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetAllItemsQueryResponse>>> Handle(GetAllItemsQueryRequest request, CancellationToken cancellationToken)
    {
        var items = await _unitOfWork.ItemRepository.GetAllAsync();

        if (items==null || items.Count()==0)
        {
            //return new Result<List<GetAllItemsQueryResponse>>(new List<string> { "No items found" });
            throw new BadRequestException("No items found");

        }

        var response = _mapper.Map<List<GetAllItemsQueryResponse>>(items);
        return new Result<List<GetAllItemsQueryResponse>> { Data = response };
    }
}
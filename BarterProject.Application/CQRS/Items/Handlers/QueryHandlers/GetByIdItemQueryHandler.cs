using AutoMapper;
using BarterProject.Application.CQRS.Items.Queries.Requests;
using BarterProject.Application.CQRS.Items.Queries.Responses;
using BarterProject.Common.Exceptions;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Handlers.QueryHandlers;

public class GetByIdItemQueryHandler : IRequestHandler<GetByIdItemQueryRequest, Result<GetByIdItemQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdItemQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetByIdItemQueryResponse>> Handle(GetByIdItemQueryRequest request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.ItemRepository.GetByIdAsync(request.Id);

        if (item == null)
        {
            //return new Result<GetByIdItemQueryResponse>(new List<string> { "Item not found" });
            throw new BadRequestException("Items not found");
        }

        var response = _mapper.Map<GetByIdItemQueryResponse>(item);
        return new Result<GetByIdItemQueryResponse> { Data = response };
    }
}
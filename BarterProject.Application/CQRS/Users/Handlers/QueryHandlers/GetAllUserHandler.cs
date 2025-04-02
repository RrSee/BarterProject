using AutoMapper;
using BarterProject.Application.CQRS.Comments.Queries.Responses;
using BarterProject.Application.CQRS.Users.Queries.Requests;
using BarterProject.Application.CQRS.Users.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Handlers.QueryHandlers;

public class GetAllUserHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllUserRequest, Result<List<GetAllUserResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<GetAllUserResponse>>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.GetAllInitialDataAsync();
        var response = users.Select(x => _mapper.Map<GetAllUserResponse>(x)).ToList();

        return new Result<List<GetAllUserResponse>>
        {
            Data = response,
            Errors = [],
            IsSuccess = true
        };
    }
}
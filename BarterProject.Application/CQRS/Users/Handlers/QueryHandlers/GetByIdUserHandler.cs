using AutoMapper;
using BarterProject.Application.CQRS.Users.Queries.Requests;
using BarterProject.Application.CQRS.Users.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Handlers.QueryHandlers;

public class GetByIdUserHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByIdUserRequest, Result<GetByIdUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<GetByIdUserResponse>> Handle(GetByIdUserRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        if (currentUser == null)
        {
            throw new Exception("User is not exist with provided Id");
        }

        var result = _mapper.Map<GetByIdUserResponse>(currentUser);

        return new Result<GetByIdUserResponse>
        {
            Data = result,
            Errors = [],
            IsSuccess = true
        };
    }
}
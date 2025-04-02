using AutoMapper;
using BarterProject.Application.CQRS.Users.Queries.Requests;
using BarterProject.Application.CQRS.Users.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Handlers.QueryHandlers;

public class GetByEmailUserHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByEmailUserRequest, Result<GetByEmailUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<GetByEmailUserResponse>> Handle(GetByEmailUserRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
        if (currentUser == null)
        {
            throw new Exception("User is not exist with provided Id");
        }

        var result = _mapper.Map<GetByEmailUserResponse>(currentUser);

        return new Result<GetByEmailUserResponse>
        {
            Data = result,
            Errors = [],
            IsSuccess = true
        };
    }
}
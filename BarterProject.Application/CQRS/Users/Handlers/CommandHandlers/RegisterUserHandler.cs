using AutoMapper;
using BarterProject.Application.CQRS.Users.Commads.Requests;
using BarterProject.Application.CQRS.Users.Commads.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Common.Security;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Handlers.CommandHandlers;

public class RegisterUserHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<RegisterUserRequest, Result<RegisterUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<RegisterUserResponse>> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var isExist = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
        if (isExist != null) throw new Exception("User already registered  with provided email!"); //----------------------
        var newUser = _mapper.Map<User>(request);
        var hashPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);

        newUser.PasswordHash = hashPassword;

        await _unitOfWork.UserRepository.RegisterAsync(newUser);

        var response = _mapper.Map<RegisterUserResponse>(newUser);
        return new Result<RegisterUserResponse>
        {
            Data = response,
            IsSuccess = true,
            Errors = []
        };
    }
}

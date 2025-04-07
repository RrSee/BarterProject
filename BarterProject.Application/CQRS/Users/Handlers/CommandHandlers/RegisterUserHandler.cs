using AutoMapper;
using BarterProject.Application.CQRS.Users.Commads.Requests;
using BarterProject.Application.CQRS.Users.Commads.Responses;
using BarterProject.Application.Services.Logging_Service;
using BarterProject.Common.Exceptions;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Common.Security;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Handlers.CommandHandlers;

public class RegisterUserHandler(IUnitOfWork unitOfWork, IMapper mapper, ILoggerService logger) : IRequestHandler<RegisterUserRequest, Result<RegisterUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ILoggerService _logger = logger;

    public async Task<Result<RegisterUserResponse>> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInfo($"Registering attempt  : {request.Email}");
        var isExist = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
        if (isExist != null)
        {
            _logger.LogWarning($"User already registered with provided email! {request.Email}");
            throw new BadRequestException("User already registered  with provided email!");
        }
        var newUser = _mapper.Map<User>(request);
        var hashPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);

        newUser.PasswordHash = hashPassword;

        await _unitOfWork.UserRepository.RegisterAsync(newUser);

        var response = _mapper.Map<RegisterUserResponse>(newUser);

        _logger.LogInfo($"User registered website successfully: {request.Email}");
        return new Result<RegisterUserResponse>
        {
            Data = response,
            IsSuccess = true,
            Errors = []
        };

    }
}

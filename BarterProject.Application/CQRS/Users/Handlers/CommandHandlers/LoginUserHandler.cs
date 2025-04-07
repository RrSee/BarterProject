using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BarterProject.Application.CQRS.Users.Commads.Requests;
using BarterProject.Application.CQRS.Users.Commads.Responses;
using BarterProject.Application.Services;
using BarterProject.Application.Services.Logging_Service;
using BarterProject.Common.Exceptions;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Common.Security;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BarterProject.Application.CQRS.Users.Handlers.CommandHandlers;

public class LoginUserHandler(IUnitOfWork unitOfWork, IConfiguration configuration, ILoggerService logger) : IRequestHandler<LoginUserRequest, Result<LoginUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IConfiguration _configuration = configuration;
    private readonly ILoggerService _logger = logger;

    public async Task<Result<LoginUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInfo($"Logging attempt  : {request.Email}");
        User user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            _logger.LogWarning($"User does not exist with email: {request.Email}");
            throw new BadRequestException($"Invalid Email : {request.Email}");
        }

        var hashedPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);

        if (user.PasswordHash != hashedPassword)
        {
            _logger.LogWarning($"Log in Fail due to password does not match");
            throw new BadRequestException("Invalid password");
        }

        if (user != null && user.PasswordHash == hashedPassword)
        {
            List<Claim> authClaim = [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.Telephone)
                ];

            JwtSecurityToken token = TokenService.CreateToken(authClaim, _configuration);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            string refreshToken = TokenService.GenerateRefreshToken();

            LoginUserResponse response = new() { AccessToken = tokenString, RefreshToken = refreshToken };

            RefreshToken saveRefreshToken = new()
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpirationDate = DateTime.Now.AddDays(double.Parse(configuration.GetSection("JWT:RefreshTokenExpirationDays").Value!))
            };

            await _unitOfWork.RefreshTokenRepository.SaveRefreshToken(saveRefreshToken);
            await _unitOfWork.CommitAsync();

            _logger.LogInfo($"User entered website successfully: {request.Email}");

            return new Result<LoginUserResponse>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }

        _logger.LogWarning($"Log in Fail with any unknown reason");

        return new Result<LoginUserResponse>
        {
            Data = null,
            Errors = ["Login is failed"],
            IsSuccess = false
        };
    }
}
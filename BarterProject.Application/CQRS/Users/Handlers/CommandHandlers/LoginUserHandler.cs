using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BarterProject.Application.CQRS.Users.Commads.Requests;
using BarterProject.Application.CQRS.Users.Commads.Responses;
using BarterProject.Application.Services;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Common.Security;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace BarterProject.Application.CQRS.Users.Handlers.CommandHandlers;

public class LoginUserHandler(IUnitOfWork unitOfWork, IConfiguration configuration) : IRequestHandler<LoginUserRequest, Result<LoginUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IConfiguration _configuration = configuration;

    public async Task<Result<LoginUserResponse>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        User user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            throw new Exception($"Invalid Email : {request.Email}"); //-----------------
        }

        var hashedPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);

        if (user.PasswordHash != hashedPassword)
        {
            throw new Exception("Invalid password"); //----------------
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

            return new Result<LoginUserResponse>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
        return new Result<LoginUserResponse>
        {
            Data = null,
            Errors = ["Login is failed"],
            IsSuccess = false
        };

    }
}
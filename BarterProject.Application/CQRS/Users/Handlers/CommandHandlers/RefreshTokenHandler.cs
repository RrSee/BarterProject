using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BarterProject.Application.CQRS.Users.Commads.Requests;
using BarterProject.Application.Services;
using BarterProject.Common.GlobalResponses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace BarterProject.Application.CQRS.Users.Handlers.CommandHandlers;

public class RefreshTokenHandler(IUnitOfWork unitOfWork, IConfiguration configuration) : IRequestHandler<RefreshTokenRequest, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IConfiguration _configuration = configuration;

    public async Task<Result<string>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var refreshToken = await _unitOfWork.RefreshTokenRepository.GetRefreshToken(request.Token);
        if (refreshToken == null || refreshToken.ExpirationDate < DateTime.Now)
        {
            throw new UnauthorizedAccessException();
        }
        var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(refreshToken.UserId);


        List<Claim> authClaim = [
            new Claim(ClaimTypes.NameIdentifier, currentUser.Id.ToString()),
            new Claim(ClaimTypes.Name, currentUser.Name),
            new Claim(ClaimTypes.Email, currentUser.Email),
            new Claim(ClaimTypes.MobilePhone, currentUser.Telephone)
            ];

        JwtSecurityToken token = TokenService.CreateToken(authClaim, _configuration);
        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new Result<string> { Data = tokenString };
    }
}
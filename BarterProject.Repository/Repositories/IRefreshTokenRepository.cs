using BarterProject.Domain.Entites;

namespace BarterProject.Repository.Repositories;

public interface IRefreshTokenRepository
{
    Task SaveRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken> GetRefreshToken(string token);
}

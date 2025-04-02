using BarterProject.DAL.SqlServer.Context;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarterProject.DAL.SqlServer.Infrastructure;

public class SqlRefreshTokenRepository(AppDbContext context) : IRefreshTokenRepository
{
    private readonly AppDbContext _context = context;

    public async Task<RefreshToken> GetRefreshToken(string token)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
    }

    public async Task SaveRefreshToken(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
    }
}

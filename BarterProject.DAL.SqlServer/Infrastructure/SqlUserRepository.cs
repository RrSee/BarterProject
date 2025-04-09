using BarterProject.DAL.SqlServer.Context;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarterProject.DAL.SqlServer.Infrastructure;

public class SqlUserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddFavoriteItemAsync(UsersFavoriteItems favoriteItem)
    {
        favoriteItem.CreatedDate = DateTime.Now;
        await _context.UsersFavoriteItems.AddAsync(favoriteItem);
        await _context.SaveChangesAsync();
    }

    public IQueryable<User> GetAll()
    {
        return _context.Users.Where(u => u.IsDeleted == false);
    }

    public async Task<IEnumerable<User>> GetAllInitialDataAsync()
    {
        return await _context.Users.Where(u => u.IsDeleted == false).ToListAsync();
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsDeleted == false);
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);
    }

    public async Task RegisterAsync(User user)
    {
        user.CreatedDate = DateTime.Now;
        user.CreatedBy = user.Id;
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Remove(int id, int deletedBy)
    {
        var currentUser = _context.Users.FirstOrDefault(u => u.Id == id);
        if (currentUser == null)
        {
            return false;
        }
        currentUser.DeletedDate = DateTime.Now;
        currentUser.IsDeleted = true;
        currentUser.DeletedBy = deletedBy;
        _context.Users.Update(currentUser);
        _context.SaveChanges();
        return true;
    }

    public void Update(User user)
    {
        user.UpdatedDate = DateTime.Now;
        user.UpdatedBy = user.Id;
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}
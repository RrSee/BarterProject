using BarterProject.Domain.Entites;

namespace BarterProject.Repository.Repositories;

public interface IUserRepository
{
    Task RegisterAsync(User user);
    void Update(User user);
    Task<bool> Remove(int id, int deletedBy);
    IQueryable<User> GetAll();
    Task<IEnumerable<User>> GetAllInitialDataAsync();
    Task<User> GetByIdAsync(int id);
    Task<User> GetByEmailAsync(string email);
}
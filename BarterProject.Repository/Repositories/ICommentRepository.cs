using BarterProject.Domain.Entites;

namespace BarterProject.Repository.Repositories;

public interface ICommentRepository
{
    Task AddAsync(Comment comment);
    void Update(Comment comment);
    Task<bool> Delete(int id, int deletedBy);
    IQueryable<Comment> GetAll();
    Task<Comment> GetByIdAsync(int id);
    Task<IEnumerable<Comment>> GetAllInitialDataAsync();
    Task<IEnumerable<Comment>> GetByItemIdInitialDataAsync(int itemId);
    IQueryable<Comment> GetByItemId(int itemId);
}

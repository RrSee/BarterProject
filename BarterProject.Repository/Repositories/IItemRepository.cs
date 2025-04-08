using BarterProject.Domain.Entites;

namespace BarterProject.Repository.Repositories;

public interface IItemRepository
{
    Task<Item> AddAsync(Item item);
    Task<bool> UpdateAsync(Item item);
    Task<bool> DeleteAsync(int id,int deletedBy);
    Task<Item?> GetByIdAsync(int id);
    Task<IEnumerable<Item>> GetAllAsync();
    Task<IEnumerable<Item>> GetByUserIdAsync(int userId);
    Task<List<Item>> SearchByNameAsync(string keyword);
    Task<List<Item>> GetByCategoryIdAsync(int categoryId);


}

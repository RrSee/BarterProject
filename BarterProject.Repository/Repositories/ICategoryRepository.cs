using BarterProject.Domain.Entites;

namespace BarterProject.Repository.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();  
    Task<Category> GetByIdAsync(int id); 
    Task<Category> AddAsync(Category category);  
    Task<bool> UpdateAsync(Category category); 
    Task<bool> DeleteAsync(int id,int deletedBy); 

}

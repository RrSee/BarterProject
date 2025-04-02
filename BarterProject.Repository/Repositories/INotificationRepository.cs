using BarterProject.Domain.Entites;

namespace BarterProject.Repository.Repositories;

public interface INotificationRepository
{
    Task AddAsync(Notification notification); 
    Task<Notification> GetByIdAsync(int id); 
    Task<List<Notification>> GetAllAsync(); 
    Task<List<Notification>> GetByUserIdAsync(int userId); 
    Task<bool> DeleteAsync(int id,int deletedBy);
    // Yeni metodlar:
    Task<bool> UpdateAsync(Notification notification);
    Task<bool> MarkAsReadAsync(int id, int userId); // Bildirişin oxundu olaraq işarələnməsi
    Task<List<Notification>> GetUnreadNotificationsAsync(int userId); // Bütün oxunmamış bildirişləri almaq
}

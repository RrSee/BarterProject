using BarterProject.DAL.SqlServer.Context;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarterProject.DAL.SqlServer.Infrastructure;

public class SqlNotificationRepository(AppDbContext context) : INotificationRepository
{
    private readonly AppDbContext _context = context;
   
    public async Task AddAsync(Notification notification)
    {
        await _context.AddAsync(notification);
        await _context.SaveChangesAsync();
    }

    public async Task<bool>DeleteAsync(int id,int deletedBy)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification == null)
        {
            return false; 
        }
        notification.IsDeleted = true;
        notification.DeletedBy = deletedBy;
        notification.DeletedDate = DateTime.Now;
        _context.Notifications.Update(notification);
        await _context.SaveChangesAsync();
        return true; 
    }

    public async Task<List<Notification>> GetAllAsync()
    {
        return await _context.Notifications
               .Where(n => !n.IsDeleted)
               .ToListAsync();
    }

    public async Task<Notification> GetByIdAsync(int id)
    {
        return await _context.Notifications
               .FirstOrDefaultAsync(n => n.Id == id && !n.IsDeleted);
    }

    public async Task<List<Notification>> GetByUserIdAsync(int userId)
    {
        return await _context.Notifications
                 .Where(n => n.UserId == userId && !n.IsDeleted)
                 .ToListAsync();
    }

    public async Task<List<Notification>> GetUnreadNotificationsAsync(int userId)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead && !n.IsDeleted)
            .ToListAsync();
    }

    public async Task<bool> MarkAsReadAsync(int id, int userId)
    {
        var notification = await _context.Notifications
        .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId && !n.IsDeleted);

        if (notification == null)
        {
            return false; 
        }

        notification.IsRead = true;
        _context.Notifications.Update(notification);
        await _context.SaveChangesAsync();

        return true; 
    }

    public async Task<bool> UpdateAsync(Notification notification)
    {
        var existingNotification = await _context.Notifications
           .FirstOrDefaultAsync(n => n.Id == notification.Id && !n.IsDeleted);

        if (existingNotification == null)
        {
            return false;
        }

        existingNotification.Description = notification.Description;
        existingNotification.IsRead = notification.IsRead;
        existingNotification.UpdatedDate = DateTime.Now;

        _context.Notifications.Update(existingNotification);
        await _context.SaveChangesAsync();
        return true;
    }
}

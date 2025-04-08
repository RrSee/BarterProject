using BarterProject.DAL.SqlServer.Context;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarterProject.DAL.SqlServer.Infrastructure;

public class SqlItemRepository(AppDbContext context):IItemRepository
{
    private readonly AppDbContext _context = context;

    
    public async Task<Item> AddAsync(Item item)
    {
        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<bool> DeleteAsync(int id, int deletedBy)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return false;
        }

        item.IsDeleted = true;
        item.DeletedBy = deletedBy;
        item.DeletedDate = DateTime.Now;
        _context.Items.Update(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await _context.Items
            .Where(i => !i.IsDeleted)
            .ToListAsync();
    }

    public async Task<Item> GetByIdAsync(int id)
    {
        return await _context.Items
            .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
    }

    public async Task<IEnumerable<Item>> GetByUserIdAsync(int userId)
    {
        return await _context.Items
            .Where(i => i.UserId == userId && !i.IsDeleted)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(Item item)
    {
        var existingItem = await _context.Items
            .FirstOrDefaultAsync(i => i.Id == item.Id && !i.IsDeleted);

        if (existingItem == null)
            return false;

        existingItem.Name = item.Name;
        existingItem.Description = item.Description;
        existingItem.ImagePath = item.ImagePath;
        existingItem.UpdatedDate = DateTime.Now;
        existingItem.CategoryId = item.CategoryId;
        _context.Items.Update(existingItem);
        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<List<Item>> SearchByNameAsync(string keyword)
    {
        return await _context.Items
            .Where(i => i.Name.ToLower().Contains(keyword.ToLower()))
            .ToListAsync();
    }

    public async Task<List<Item>> GetByCategoryIdAsync(int categoryId)
    {
        return await _context.Items
        .Where(i => i.CategoryId == categoryId)
        .ToListAsync();
    }
}


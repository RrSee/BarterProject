using BarterProject.DAL.SqlServer.Context;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarterProject.DAL.SqlServer.Infrastructure;

public class SqlCategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly AppDbContext _context = context;
    public async Task<Category> AddAsync(Category category)
    {
       await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<bool> DeleteAsync(int id,int deletedBy)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category== null)
        {
            return false;
        }

        category.IsDeleted = true;
        category.DeletedBy = deletedBy;
        category.DeletedDate = DateTime.Now;
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories
             .Where(i => !i.IsDeleted)
             .ToListAsync();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        return await _context.Categories
           .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
    }

    public async Task<bool> UpdateAsync(Category category)
    {
        var existingCategory = await _context.Categories
            .FirstOrDefaultAsync(i => i.Id == category.Id && !i.IsDeleted);

        if (existingCategory == null)
            return false;

        existingCategory.Name = category.Name;
        existingCategory.UpdatedDate = DateTime.Now;
        _context.Categories.Update(existingCategory);
        await _context.SaveChangesAsync();

        return true;
    }
}

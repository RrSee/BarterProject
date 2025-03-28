using BarterProject.DAL.SqlServer.Context;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarterProject.DAL.SqlServer.Infrastructure;

public class SqlCommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public SqlCommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Comment comment)
    {
        await _context.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Delete(int id, int deletedBy)
    {
        var comment = await _context.Comments.FindAsync(id);
        if(comment == null)
        {
            return false;
        }

        comment.DeletedBy = deletedBy;
        comment.IsDeleted = true;
        comment.DeletedDate = DateTime.Now;

        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();
        return true;
    }

    public IQueryable<Comment> GetAll()
    {
        return _context.Comments.Where(c => c.IsDeleted == false);
    }

    public async Task<IEnumerable<Comment>> GetAllInitialDataAsync()
    {
        return await _context.Comments
            .Where(c => !c.IsDeleted).ToListAsync();
    }

    public async Task<Comment> GetByIdAsync(int id)
    {
        return await _context.Comments.FirstOrDefaultAsync(c=>c.Id == id && !c.IsDeleted);
    }

    public  IQueryable<Comment> GetByItemId(int itemId)
    {
        return _context.Comments.Where(c => c.ItemId == itemId && !c.IsDeleted);
    }

    public async Task<IEnumerable<Comment>> GetByItemIdInitialDataAsync(int itemId)
    {
        return await _context.Comments
            .Where(c => !c.IsDeleted && c.ItemId == itemId).ToListAsync();
    }

    public void Update(Comment comment)
    {
        _context.Comments.Update(comment);
        _context.SaveChanges();
    }
}

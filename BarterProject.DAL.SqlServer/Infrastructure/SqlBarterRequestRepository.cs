using BarterProject.DAL.SqlServer.Context;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Repositories;

namespace BarterProject.DAL.SqlServer.Infrastructure;

public class SqlBarterRequestRepository(AppDbContext context) : IBarterRequestRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(BarterRequest barterRequest)
    {
        await _context.BarterRequests.AddAsync(barterRequest);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Delete(int id, int deletedBy)
    {
        var barterRequest = await _context.BarterRequests.FindAsync(id);
        if (barterRequest == null)
        {
            return false;
        }

        barterRequest.IsDeleted = true;
        barterRequest.DeletedBy = deletedBy;
        barterRequest.DeletedDate = DateTime.Now;

        _context.BarterRequests.Update(barterRequest);
        await _context.SaveChangesAsync();
        return true;
    }

 
}

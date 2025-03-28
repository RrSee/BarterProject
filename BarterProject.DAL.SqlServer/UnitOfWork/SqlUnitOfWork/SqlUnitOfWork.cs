using BarterProject.DAL.SqlServer.Context;
using BarterProject.DAL.SqlServer.Infrastructure;
using BarterProject.Repository.Common;
using BarterProject.Repository.Repositories;

namespace BarterProject.DAL.SqlServer.UnitOfWork.SqlUnitOfWork;

public class SqlUnitOfWork(string connectionString, AppDbContext context) : IUnitOfWork
{
    private readonly string _connectionString = connectionString;
    private readonly AppDbContext _context = context;

    public SqlBarterRequestRepository _barterRequestRepository;
    public SqlNotificationRepository _notificationRepository;
    public SqlCommentRepository _commentRepository;

    public IBarterRequestRepository BarterRequestRepository => _barterRequestRepository ?? new SqlBarterRequestRepository(_context);

    public INotificationRepository NotificationRepository => _notificationRepository ?? new SqlNotificationRepository(_context);

    public ICommentRepository CommentRepository => _commentRepository ?? new SqlCommentRepository(_context);

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

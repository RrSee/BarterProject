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

    public IBarterRequestRepository BarterRequestRepository => BarterRequestRepository ?? new SqlBarterRequestRepository(_context);
}

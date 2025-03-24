using BarterProject.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace BarterProject.DAL.SqlServer.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<BarterRequest> BarterRequests { get; set; }
}

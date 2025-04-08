using BarterProject.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace BarterProject.DAL.SqlServer.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<BarterRequest> BarterRequests { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Category> Categories { get; set; }
}

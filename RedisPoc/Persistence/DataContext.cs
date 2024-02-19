using Microsoft.EntityFrameworkCore;
using RedisPoc.Entitites;

namespace RedisPoc.Persistence;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
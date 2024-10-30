using gRPC.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace gRPC.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ProductEntity> Products => Set<ProductEntity>();
    }
}
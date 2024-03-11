using Graphql.Entities;
using Microsoft.EntityFrameworkCore;

namespace Graphql.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<ProductDetails> Products { get; set; }
    }
}

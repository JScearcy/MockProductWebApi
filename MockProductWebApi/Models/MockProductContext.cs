using Microsoft.EntityFrameworkCore;

namespace MockProductWebApi.Models
{
    public class MockProductContext : DbContext, IMockProductDbContext
    {
        public DbSet<MockProduct> MockProducts { get; set; }
        public DbSet<MockProductReview> MockProductReviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=mockproducts.db");
        }
    }
}

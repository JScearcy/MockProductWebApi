using Microsoft.EntityFrameworkCore;

namespace MockProductWebApi.Models
{
    public interface IMockProductDbContext
    {
        int SaveChanges();
        DbSet<MockProduct> MockProducts { get; set; }
        DbSet<MockProductReview> MockProductReviews { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MockProductWebApi.Models
{
    public interface IMockProductDbContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        DbSet<MockProduct> MockProducts { get; set; }
        DbSet<MockProductReview> MockProductReviews { get; set; }
    }
}

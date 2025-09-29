using Microsoft.EntityFrameworkCore;
using TBRly.API.Models;

namespace TBRly.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; } = null!; // non sarà mai null a runtime
    }
}

using API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Entities.Program> Programs { get; set; }
        public DbSet<Entities.Question> Questions { get; set; }

        public static async Task SeedDatabaseAsync(DbContextOptions<AppDbContext> options)
        {
            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();
        }
    }
}

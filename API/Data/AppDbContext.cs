using API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<DbContext> options) : base(options) { }

        public DbSet<Entities.Program> Programs { get; set; }
        public DbSet<Entities.Question> Questions { get; set; }
        public DbSet<Entities.Answer> Answers { get; set; }
        public DbSet<Entities.Application> Applications { get; set; }

    }
}

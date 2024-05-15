using API.Data;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest;
public static class ServicesMock
{
    public static IServiceCollection GetCollection()
    {
        IServiceCollection services = new ServiceCollection();

        //database
        services.AddScoped<AppDbContext, TestDbContext>();
        services.AddScoped<IProgramService,  ProgramService>();
        services.AddScoped<IApplicationService,  ApplicationService>();

        return services;
    }
}

public class TestDbContext : AppDbContext
{
    public TestDbContext(DbContextOptions<DbContext> options) : base(options)
    {
    }
    public TestDbContext()
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Program>().HasKey(x => x.Id);
        modelBuilder.Entity<Question>().HasKey(x => x.Id);
        modelBuilder.Entity<Answer>().HasKey(x => x.Id);
        modelBuilder.Entity<Application>().HasKey(x => x.Id);
    }
}
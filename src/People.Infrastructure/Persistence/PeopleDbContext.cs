using Microsoft.EntityFrameworkCore;
using People.Domain.Entities;

namespace People.Infrastructure.Persistence;

public class PeopleDbContext : DbContext
{
    public PeopleDbContext(DbContextOptions<PeopleDbContext> options) : base(options)
    {
    }

    public DbSet<Person> People => Set<Person>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("People");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Role).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Department).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20).HasDefaultValue("active");
            entity.Property(e => e.Summary).HasMaxLength(500);
            entity.Property(e => e.CreatedAtUtc).IsRequired();
            entity.Property(e => e.LastUpdatedAtUtc).IsRequired();
        });
    }
}

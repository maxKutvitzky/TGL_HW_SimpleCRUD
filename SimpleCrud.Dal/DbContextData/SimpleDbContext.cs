using Microsoft.EntityFrameworkCore;
using SimpleCrud.Model;

namespace SimpleCrud.Dal.DbContextData;

public class SimpleDbContext : DbContext
{
    public SimpleDbContext(DbContextOptions<SimpleDbContext> options) : base(options)
    {
    }

    #region DbSets

    public DbSet<Student>? Students { get; set; }
    public DbSet<Group>? Groups { get; set; }
    public DbSet<Subject>? Subjects { get; set; }
    public DbSet<Passport>? Passports { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasMany(g => g.Students)
                .WithOne(s => s.Group)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleCrud.Model;

namespace SimpleCrud.Dal.DbContextData
{
    public class SimpleDbContext : DbContext
    {
        public SimpleDbContext(DbContextOptions<SimpleDbContext> options):base(options){}
        public DbSet<Student>? Students { get; set; }
        public DbSet<Group>? Groups { get; set; }
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
}

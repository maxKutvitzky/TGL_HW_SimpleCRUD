using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleCrud.Dal.DbContextData;

internal class SimpleDbContextFactoryI : IDesignTimeDbContextFactory<SimpleDbContext>
{
    public SimpleDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SimpleDbContext>();
        var connectionString =
            @"Server=localhost; Database=SimpleCRUD; TrustServerCertificate=True; Trusted_Connection=true";
        optionsBuilder.UseSqlServer(connectionString);
        return new SimpleDbContext(optionsBuilder.Options);
    }
}
using Microsoft.EntityFrameworkCore;
using SimpleCrud.Dal.DbContextData;
using SimpleCrud.Dal.Repositories.Base;
using SimpleCrud.Dal.Repositories.Interfaces;
using SimpleCrud.Model;

namespace SimpleCrud.Dal.Repositories;

public class GroupRepository : BaseRepo<Group>, IGroupRepository
{
    public GroupRepository(SimpleDbContext dnContext) : base(dnContext)
    {
    }

    public override Group GetById(int? id)
    {
        return dbSet
            .Where(g => g.Id == id)
            .Include(g => g.Students)
            .FirstOrDefault();
    }

    public override IEnumerable<Group> GetAll()
    {
        return dbSet
            .Include(g => g.Students)
            .OrderBy(g => g.Name);
    }
}
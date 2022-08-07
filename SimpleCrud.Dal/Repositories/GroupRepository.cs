using Microsoft.EntityFrameworkCore;
using SimpleCrud.Dal.DbContextData;
using SimpleCrud.Dal.Repositories.Base;
using SimpleCrud.Dal.Repositories.Interfaces;
using SimpleCrud.Model;

namespace SimpleCrud.Dal.Repositories
{
    internal class GroupRepository : BaseRepo<Group>, IGroupRepository
    {
        public GroupRepository(SimpleDbContext dnContext) : base(dnContext)
        {
        }

        public override IEnumerable<Group> GetAll() =>
            dbSet
                .Include(g => g.Students)
                .OrderBy(g => g.Name);

    }
}

using SimpleCrud.Dal.DbContextData;
using SimpleCrud.Dal.Repositories.Base;
using SimpleCrud.Dal.Repositories.Interfaces;
using SimpleCrud.Model;

namespace SimpleCrud.Dal.Repositories
{
    public class SubjectRepository : BaseRepo<Subject>, ISubjectRepository
    {
        public SubjectRepository(SimpleDbContext dnContext) : base(dnContext)
        {
        }

        public override Subject GetById(int? id)
        {
            return dbSet.Find(id);
        }

        public override IEnumerable<Subject> GetAll()
        {
            return dbSet;
        }
    }
}

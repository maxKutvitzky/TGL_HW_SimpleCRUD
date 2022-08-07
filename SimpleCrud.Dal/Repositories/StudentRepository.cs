using Microsoft.EntityFrameworkCore;
using SimpleCrud.Dal.DbContextData;
using SimpleCrud.Dal.Repositories.Base;
using SimpleCrud.Dal.Repositories.Interfaces;
using SimpleCrud.Model;

namespace SimpleCrud.Dal.Repositories
{
    internal class StudentRepository: BaseRepo<Student>, IStudentRepository
    {
        public StudentRepository(SimpleDbContext dnContext) : base(dnContext)
        {
        }

        public override IEnumerable<Student> GetAll() =>
            dbSet
                .Include(s => s.Group)
                .OrderBy(s => s.LastName);

        public IEnumerable<Student> GetAllByGroupName(string name) =>
            dbSet
                .Where(s => s.Group.Name == name)
                .Include(s => s.Group)
                .OrderBy(s => s.LastName);

        public IEnumerable<Student> GetAllByGroupId(int id) =>
            dbSet
                .Where(s => s.Group.Id == id)
                .Include(s => s.Group)
                .OrderBy(s => s.LastName);
    }
}

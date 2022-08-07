using SimpleCrud.Dal.Repositories.Base;
using SimpleCrud.Model;

namespace SimpleCrud.Dal.Repositories.Interfaces;

public interface IStudentRepository : IBaseRepo<Student>
{
    public IEnumerable<Student> GetAllByGroupName(string name);
    public IEnumerable<Student> GetAllByGroupId(int id);
}
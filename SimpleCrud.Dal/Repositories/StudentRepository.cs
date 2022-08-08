using Microsoft.EntityFrameworkCore;
using SimpleCrud.Dal.DbContextData;
using SimpleCrud.Dal.Repositories.Base;
using SimpleCrud.Dal.Repositories.Interfaces;
using SimpleCrud.Model;

namespace SimpleCrud.Dal.Repositories;

public class StudentRepository : BaseRepo<Student>, IStudentRepository
{
    public StudentRepository(SimpleDbContext dbContext) : base(dbContext)
    {
    }

    public override Student GetById(int? id)
    {
        return dbSet
            .Where(s => s.Id == id)
            .Include(s => s.Group)
            .FirstOrDefault();
    }

    public override IEnumerable<Student> GetAll()
    {
        return dbSet
            .Include(s => s.Group)
            .OrderBy(s => s.Group.Name);
    }

    public override int Update(Student entity)
    {
        var student = dbSet.Find(entity.Id);
        if (student == null) return 0;
        student.FirstName = entity.FirstName;
        student.LastName = entity.LastName;
        student.Group = entity.Group;
        student.Email = entity.Email;
        student.PhoneNumber = entity.PhoneNumber;
        student.Gender = entity.Gender;
        student.PaymentPlan = entity.PaymentPlan;
        dbSet.Update(student);
        return dbContext.SaveChanges();
    }
}
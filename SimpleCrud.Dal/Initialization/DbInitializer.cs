using Microsoft.EntityFrameworkCore;
using SimpleCrud.Dal.DbContextData;
using SimpleCrud.Model;

namespace SimpleCrud.Dal.Initialization;

public static class DbInitializer
{
    private static void RecreateDatabase(SimpleDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.Migrate();
    }

    private static void SeedData(SimpleDbContext context, int studentsCount)
    {
        context.Subjects?.AddRange(InitData.Subjects);
        context.SaveChanges();
        context.Groups?.AddRange(InitData.Groups);
        context.SaveChanges();
        var students = InitData
            .GenerateStudents
            (
                studentsCount,
                context.Groups.ToList(),
                context.Subjects.ToList()
            );
        SetPassports(students);
        context.Students?.AddRange(students);
        context.SaveChanges();
    }

    public static void InitializeDataBase(SimpleDbContext context, int studentsCount)
    {
        context.Database.Migrate();
        if (!context.Students.Any() && !context.Groups.Any()) SeedData(context, studentsCount);
    }

    public static void InitializeDataBaseWithRecreation(SimpleDbContext context, int studentsCount)
    {
        RecreateDatabase(context);
        SeedData(context, studentsCount);
    }

    private static void SetPassports(List<Student> students)
    {
        var passports = InitData.GeneratePassports(students.Count).ToList();
        foreach (Student student in students)
        {
            student.Passport = passports[0];
            passports.Remove(passports[0]);
        }
    }
}
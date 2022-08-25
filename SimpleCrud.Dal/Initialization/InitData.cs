using Bogus;
using Bogus.DataSets;
using SimpleCrud.Model;
using SimpleCrud.Model.Enums;

namespace SimpleCrud.Dal.Initialization;

public static class InitData
{
    public static List<Subject> Subjects =>
        new()
        {
            new() { Name = "Math" },
            new() { Name = "English" },
            new() { Name = "Biology" },
            new() { Name = "History" },
            new() { Name = "Geography" }
        };
    public static List<Group> Groups =>
        new()
        {
            new() { Name = "Group1" },
            new() { Name = "Group2" },
            new() { Name = "Group3" },
            new() { Name = "Group4" }
        };

    public static List<Student> GenerateStudents(int count, List<Group> groups, List<Subject> subjects)
    {
        var students = new Faker<Student>()
            .RuleFor(s => s.Gender, f => f.PickRandom<GenderEnum>())
            .RuleFor(s => s.FirstName, (f, s) => f.Name.FirstName(GetGender(s.Gender)))
            .RuleFor(s => s.LastName, (f, s) => f.Name.LastName(GetGender(s.Gender)))
            .RuleFor(s => s.Email, (f, s) => f.Internet.Email(s.FirstName, s.LastName))
            .RuleFor(s => s.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(s => s.PaymentPlan, f => f.PickRandom<PaymentPlanEnum>())
            .RuleFor(s => s.Group, f => f.PickRandom(groups))
            .RuleFor(s => s.Subjects, f => f.PickRandom(subjects, 3).ToList());

        return students.Generate(count);
    }

    private static Name.Gender GetGender(GenderEnum gender)
    {
        return gender == 0 ? Name.Gender.Male : Name.Gender.Female;
    }

    public static IEnumerable<Passport> GeneratePassports(int count)
    {
        return new Faker<Passport>()
            .RuleFor(s => s.Number, f => f.Random.Int(0))
            .RuleFor(s => s.Address, f => f.Address.FullAddress())
            .RuleFor(s => s.DateOfBirth, f => f.Date.Past(20, new DateTime(2000, 1, 1)))
            .Generate(count);
    }
}
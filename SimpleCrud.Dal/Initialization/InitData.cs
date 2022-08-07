﻿using Bogus;
using Bogus.DataSets;
using SimpleCrud.Model;
using SimpleCrud.Model.Enums;

namespace SimpleCrud.Dal.Initialization
{
    public static class InitData
    {
        public static List<Group> Groups =>
            new List<Group>
            {
                new Group{Name = "Group1"},
                new Group{Name = "Group2"},
                new Group{Name = "Group3"},
                new Group{Name = "Group4"}
            };
        public static List<Student> GenerateStudents(int count)
        {
            var students = new Faker<Student>()
                .RuleFor(s => s.Gender, f => f.PickRandom<GenderEnum>())
                .RuleFor(s => s.FirstName, (f,s) => f.Name.FirstName(GetGender(s.Gender)))
                .RuleFor(s => s.LastName, (f,s) => f.Name.LastName(GetGender(s.Gender)))
                .RuleFor(s => s.Email, (f, s) => f.Internet.Email(s.FirstName, s.LastName))
                .RuleFor(s => s.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(s => s.PaymentPlan, f => f.PickRandom<PaymentPlan>())
                .RuleFor(s => s.Group, f => f.PickRandom(Groups));

            return students.Generate(count);
        }

        private static Name.Gender GetGender(GenderEnum gender) => 
            gender == 0 ? Name.Gender.Male : Name.Gender.Female;
    }
}

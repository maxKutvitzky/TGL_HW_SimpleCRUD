using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleCrud.Dal.DbContextData;
using SimpleCrud.Dal.Initialization;
using SimpleCrud.Dal.Repositories;
using SimpleCrud.Dal.Repositories.Interfaces;
using SimpleCrud.Model;
using SimpleCrud.Model.Enums;
using System.IO;
using System.Linq;
using Xunit;

namespace SimpleCrud.Tests
{
    public class StudentRepositoryTest
    {
        private IStudentRepository _studentRepository;
        private readonly IConfiguration _configuration;

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testConfig.json")
                .Build();
        }

        private static void RecreateTestDatabase(SimpleDbContext dbContext)
        {
            DbInitializer.InitializeDataBaseWithRecreation(dbContext, 5);
        }

        private Student CreateTestStudent()
        {
            var selectedStudent = _studentRepository.GetById(1);
            var student = new Student
            {
                Email = "test@gmail.com",
                FirstName = "FName",
                Gender = GenderEnum.Male,
                Group = selectedStudent.Group,
                Subjects = selectedStudent.Subjects,
                LastName = "LName",
                Passport = selectedStudent.Passport,
                PaymentPlan = PaymentPlanEnum.Budget,
                PhoneNumber = selectedStudent.PhoneNumber
            };
            return student;
        }
        public StudentRepositoryTest()
        {
            _configuration = GetConfiguration();

            string connectionString = _configuration.GetConnectionString("StudentTesting");

            var dbContextOptions = new DbContextOptionsBuilder<SimpleDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var dbContext = new SimpleDbContext(dbContextOptions);
            _studentRepository = new StudentRepository(dbContext);
            RecreateTestDatabase(dbContext);
        }
        [Fact]
        public void GetByIdExists_Test()
        {
            var studentById = _studentRepository.GetById(1);

            Assert.NotNull(studentById);
        }
        [Fact]
        public void GetByIdNotExists_Test()
        {
            var studentById = _studentRepository.GetById(10000);

            Assert.Null(studentById);
        }
        [Fact]
        public void GetAll_Test()
        {
            var allStudents = _studentRepository.GetAll();

            Assert.NotNull(allStudents);
            Assert.True(allStudents.Any());
            Assert.Equal(5, allStudents.Count());
        }

        [Fact]
        public void Update_Test()
        {
            string newName = "FNameTest";
            var updatedStudent = _studentRepository.GetById(1);
            string oldName = updatedStudent.FirstName;
            updatedStudent.FirstName = newName;
            _studentRepository.Update(updatedStudent);

            Assert.Equal(newName, _studentRepository.GetById(1).FirstName);

            updatedStudent.FirstName = oldName;
            _studentRepository.Update(updatedStudent);
        }

        [Fact]
        public void Create_Test()
        {
            var student = CreateTestStudent();
            _studentRepository.Add(student);
            var allStudents = _studentRepository.GetAll();

            Assert.Contains(allStudents, s => s.FirstName == "FName" &&
                                              s.Email == "test@gmail.com" &&
                                              s.LastName == "LName");

            _studentRepository.Delete(student);
        }
    }
}

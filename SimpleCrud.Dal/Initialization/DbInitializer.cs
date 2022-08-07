using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleCrud.Dal.DbContextData;

namespace SimpleCrud.Dal.Initialization
{
    public static class DbInitializer
    {
        private static void RecreateDatabase(SimpleDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        private static void SeedData(SimpleDbContext context, int studentsCount)
        {
            context.Groups?.AddRange(InitData.Groups);
            context.Students?.AddRange(InitData.GenerateStudents(studentsCount));
            context.SaveChanges();
        }

        public static void InitializeDataBase(SimpleDbContext context, int studentsCount)
        {
            context.Database.Migrate();
            SeedData(context,studentsCount);
        }
        public static void InitializeDataBaseWithRecreation(SimpleDbContext context, int studentsCount)
        {
            RecreateDatabase(context);
            SeedData(context, studentsCount);
        }
    }
}

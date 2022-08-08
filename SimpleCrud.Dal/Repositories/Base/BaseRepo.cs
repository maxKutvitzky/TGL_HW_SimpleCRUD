using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleCrud.Dal.DbContextData;
using SimpleCrud.Model.Base;

namespace SimpleCrud.Dal.Repositories.Base
{
    public abstract class BaseRepo<T> : IBaseRepo<T> where T : BaseEntity, new()
    {
        protected SimpleDbContext dbContext;
        protected DbSet<T> dbSet;
        protected BaseRepo(SimpleDbContext dnContext)
        {
            this.dbContext = dnContext;
            dbSet = dnContext.Set<T>();
        }
        public int Add(T entity)
        {
            dbSet.Add(entity);
            return dbContext.SaveChanges();
        }

        public virtual int Update(T entity)
        {
            dbSet.Update(entity);
            return dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            dbSet.Remove(entity);
            return dbContext.SaveChanges();
        }

        public abstract T GetById(int? id);

        public abstract IEnumerable<T> GetAll();
    }
}

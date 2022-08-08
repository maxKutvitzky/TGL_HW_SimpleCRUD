namespace SimpleCrud.Dal.Repositories.Base;

public interface IBaseRepo<T>
{
    int Add(T entity);
    int Update(T entity);
    int Delete(T entity);
    T GetById(int? id);
    IEnumerable<T> GetAll();
}
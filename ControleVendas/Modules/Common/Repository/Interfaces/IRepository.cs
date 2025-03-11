using System.Linq.Expressions;

namespace ControleVendas.Modules.Common.Repository.Interfaces;

public interface IRepository<T>
{
    protected IQueryable<T> GetIQueryable();
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    void Update(T entity);
    T Delete(T entity);
}
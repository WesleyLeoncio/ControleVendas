using System.Linq.Expressions;
using controle_vendas.infra.data;
using controle_vendas.modules.common.repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace controle_vendas.modules.common.repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbConnectionContext _context;

    public Repository(AppDbConnectionContext context)
    {
        _context = context;
    }
    
    public IQueryable<T> GetIQueryable()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await GetIQueryable().ToListAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await GetIQueryable().FirstOrDefaultAsync(predicate);
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;
    }

    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        return entity;
    }
}
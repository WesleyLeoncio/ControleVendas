using controle_vendas.infra.data;
using controle_vendas.modules.categoria.repository;
using controle_vendas.modules.categoria.repository.@interface;
using controle_vendas.modules.common.unit_of_work.interfaces;

namespace controle_vendas.modules.common.unit_of_work;

public class UnitOfWork : IUnitOfWork
{
    
    private ICategoriaRepository? _categoriaRepository;
    private readonly AppDbConnectionContext _context;

    public UnitOfWork(AppDbConnectionContext context)
    {
        _context = context;
    }
    
    public ICategoriaRepository CategoriaRepository
    {
        get { return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context); }
    }
    
    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}
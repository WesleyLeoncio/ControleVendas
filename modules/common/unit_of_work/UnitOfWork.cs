using controle_vendas.infra.data;
using controle_vendas.modules.common.unit_of_work.interfaces;

namespace controle_vendas.modules.common.unit_of_work;

public class UnitOfWork : IUnitOfWork
{
   
    private readonly AppDbConnectionContext _context;

    public UnitOfWork(AppDbConnectionContext context)
    {
        _context = context;
    }

    
    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}
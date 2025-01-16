using controle_vendas.infra.data;
using controle_vendas.modules.common.pagination.models.request;
using controle_vendas.modules.common.repository;
using controle_vendas.modules.fornecedor.model.entity;
using controle_vendas.modules.fornecedor.model.request;
using controle_vendas.modules.fornecedor.repository.interfaces;
using X.PagedList;
using X.PagedList.Extensions;

namespace controle_vendas.modules.fornecedor.repository;

public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
{
    public FornecedorRepository(AppDbConnectionContext context) : base(context)
    {
    }
    
    
    public async Task<IPagedList<Fornecedor>> GetAllFilterPageableAsync(FornecedorFiltroRequest filtroRequest)
    {
        IEnumerable<Fornecedor> fornecedores = await GetAllAsync();
        IQueryable<Fornecedor> queryableFornecedor = 
            fornecedores.OrderBy(f => f.Nome).AsQueryable();
        
        if (!string.IsNullOrEmpty(filtroRequest.Nome))
        {
            queryableFornecedor = queryableFornecedor.Where(f =>
                f.Nome != null && f.Nome.Contains(filtroRequest.Nome));
        }
        
        return queryableFornecedor.ToPagedList(filtroRequest.PageNumber,
            filtroRequest.PageSize);
        
    }
}
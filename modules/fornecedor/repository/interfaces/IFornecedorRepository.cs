using controle_vendas.modules.common.repository.interfaces;
using controle_vendas.modules.fornecedor.model.entity;
using controle_vendas.modules.fornecedor.model.request;
using X.PagedList;

namespace controle_vendas.modules.fornecedor.repository.interfaces;

public interface IFornecedorRepository : IRepository<Fornecedor>
{
    Task<IPagedList<Fornecedor>> GetAllFilterPageableAsync(FornecedorFiltroRequest filtroRequest);
}
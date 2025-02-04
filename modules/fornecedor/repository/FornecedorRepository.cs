using controle_vendas.infra.data;
using controle_vendas.modules.common.pagination.models.request;
using controle_vendas.modules.common.repository;
using controle_vendas.modules.fornecedor.model.entity;
using controle_vendas.modules.fornecedor.model.request;
using controle_vendas.modules.fornecedor.repository.filter;
using controle_vendas.modules.fornecedor.repository.interfaces;
using X.PagedList;
using X.PagedList.Extensions;

namespace controle_vendas.modules.fornecedor.repository;

public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
{
    public FornecedorRepository(AppDbConnectionContext context) : base(context)
    {
    }


    public Task<IPagedList<Fornecedor>> GetAllFilterPageableAsync(FornecedorFiltroRequest filtroRequest)
    {
        IQueryable<Fornecedor> fornecedorQuery = GetIQueryable();

        fornecedorQuery = FilterFornecedorName.RunFilterName(fornecedorQuery, filtroRequest.Nome);

        return Task.FromResult(fornecedorQuery.ToPagedList(filtroRequest.PageNumber,
            filtroRequest.PageSize));
    }
}
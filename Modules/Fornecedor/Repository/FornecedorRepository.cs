using ControleVendas.Infra.Data;
using ControleVendas.Modules.Common.Repository;
using ControleVendas.Modules.Fornecedor.Models.Entity;
using ControleVendas.Modules.Fornecedor.Models.Request;
using ControleVendas.Modules.Fornecedor.Repository.Filter;
using ControleVendas.Modules.Fornecedor.Repository.Interfaces;
using X.PagedList;
using X.PagedList.Extensions;

namespace ControleVendas.Modules.Fornecedor.Repository;

public class FornecedorRepository : Repository<FornecedorEntity>, IFornecedorRepository
{
    public FornecedorRepository(AppDbConnectionContext context) : base(context)
    {
    }


    public Task<IPagedList<FornecedorEntity>> GetAllFilterPageableAsync(FornecedorFiltroRequest filtroRequest)
    {
        IQueryable<FornecedorEntity> fornecedorQuery = GetIQueryable();

        fornecedorQuery = FilterFornecedorName.RunFilterName(fornecedorQuery, filtroRequest.Nome);

        return Task.FromResult(fornecedorQuery.ToPagedList(filtroRequest.PageNumber,
            filtroRequest.PageSize));
    }
}
using ControleVendas.Modules.Common.Repository.Interfaces;
using ControleVendas.Modules.Fornecedor.Models.Request;
using X.PagedList;

namespace ControleVendas.Modules.Fornecedor.Repository.Interfaces;

public interface IFornecedorRepository : IRepository<Models.Entity.Fornecedor>
{
    Task<IPagedList<Models.Entity.Fornecedor>> GetAllFilterPageableAsync(FornecedorFiltroRequest filtroRequest);
}
using ControleVendas.Modules.Common.Repository.Interfaces;
using ControleVendas.Modules.Fornecedor.Models.Entity;
using ControleVendas.Modules.Fornecedor.Models.Request;
using X.PagedList;

namespace ControleVendas.Modules.Fornecedor.Repository.Interfaces;

public interface IFornecedorRepository : IRepository<FornecedorEntity>
{
    Task<IPagedList<FornecedorEntity>> GetAllFilterPageableAsync(FornecedorFiltroRequest filtroRequest);
}
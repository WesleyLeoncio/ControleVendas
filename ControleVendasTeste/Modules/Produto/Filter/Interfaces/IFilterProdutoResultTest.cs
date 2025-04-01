using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Request;

namespace ControleVendasTeste.Modules.Produto.Filter.Interfaces;

public interface IFilterProdutoResultTest
{
    public List<ProdutoEntity> RunFilter(List<ProdutoEntity> produtos, ProdutoFiltroRequest filtro);
}
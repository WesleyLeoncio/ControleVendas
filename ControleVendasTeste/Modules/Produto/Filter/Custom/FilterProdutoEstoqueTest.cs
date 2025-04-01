using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Request;
using ControleVendasTeste.Modules.Produto.Filter.Interfaces;

namespace ControleVendasTeste.Modules.Produto.Filter.Custom;

public class FilterProdutoEstoqueTest : IFilterProdutoResultTest
{
    public List<ProdutoEntity> RunFilter(List<ProdutoEntity> produtos, ProdutoFiltroRequest filtro)
    {
       return produtos
           .Where(p => p.Estoque > 0)
           .ToList();
    }
}
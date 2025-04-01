using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Request;
using ControleVendasTeste.Modules.Produto.Filter.Interfaces;

namespace ControleVendasTeste.Modules.Produto.Filter.Custom;

public class FilterNameProdutoTest : IFilterProdutoResultTest
{
    public List<ProdutoEntity> RunFilter(List<ProdutoEntity> produtos, ProdutoFiltroRequest filtro)
    {
        if (!string.IsNullOrEmpty(filtro.Nome))
        {
            return produtos
                .Where(p => p.Nome != null && p.Nome.Contains(filtro.Nome, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        return produtos;
    }
}


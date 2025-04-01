using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Request;
using ControleVendasTeste.Modules.Produto.Filter.Interfaces;

namespace ControleVendasTeste.Modules.Produto.Filter.Custom;

public class FilterCategoriaProdutoTest : IFilterProdutoResultTest
{
    public List<ProdutoEntity> RunFilter(List<ProdutoEntity> produtos, ProdutoFiltroRequest filtro)
    {
        if (filtro.Categoria.HasValue)
        {
            return produtos.Where(p => p.CategoriaId == filtro.Categoria)
                .ToList();
        }
        return produtos;
    }
}


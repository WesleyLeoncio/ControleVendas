using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Request;
using ControleVendasTeste.Modules.Produto.Filter.Interfaces;

namespace ControleVendasTeste.Modules.Produto.Filter.Custom;

public class FilterFonecedorProdutoTest : IFilterProdutoResultTest
{
    public List<ProdutoEntity> RunFilter(List<ProdutoEntity> produtos, ProdutoFiltroRequest filtro)
    {
        if (filtro.Fornecedor.HasValue)
        {
            return produtos.Where(p => p.FornecedorId == filtro.Fornecedor).ToList();
            
        }
        return produtos;
    }
}


using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Enums;
using ControleVendas.Modules.Produto.Models.Request;
using ControleVendasTeste.Modules.Produto.Filter.Interfaces;

namespace ControleVendasTeste.Modules.Produto.Filter.Custom;

public class FilterPrecoCriterioProdutoTest : IFilterProdutoResultTest
{
    public List<ProdutoEntity> RunFilter(List<ProdutoEntity> produtos, ProdutoFiltroRequest filtro)
    {
        if (filtro.VerificarValores())
        {
           return produtos.Where(p => filtro.PrecoCriterio switch
               {
                   Criterio.Maior => p.ValorVenda > filtro.Preco,
                   Criterio.Menor => p.ValorVenda < filtro.Preco,
                   Criterio.Igual => p.ValorVenda == filtro.Preco,
                   _ => true // Caso padrão (sem filtro)
               })
               .ToList();
        }
        return produtos;
    }
}
using controle_vendas.infra.data;
using controle_vendas.modules.categoria.model;
using controle_vendas.modules.common.repository;
using controle_vendas.modules.produto.models.entity;
using controle_vendas.modules.produto.models.enums;
using controle_vendas.modules.produto.models.request;
using controle_vendas.modules.produto.repository.interfaces;
using X.PagedList;
using X.PagedList.Extensions;

namespace controle_vendas.modules.produto.repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbConnectionContext context) : base(context)
    {
    }

    public async Task<IPagedList<Produto>> GetAllFilterPageableAsync(ProdutoFiltroRequest filtroRequest)
    {
        IEnumerable<Produto> produtos = await GetAllAsync();

        IQueryable<Produto> produtosQuery =
            produtos.OrderBy(p => p.Nome).AsQueryable();

        if (!string.IsNullOrEmpty(filtroRequest.Nome))
        {
            produtosQuery = produtosQuery.Where(p =>
                p.Nome != null && p.Nome.Contains(filtroRequest.Nome));
        }

        if (filtroRequest.Categoria.HasValue)
        {
            produtosQuery = produtosQuery
                .Where(p => p.CategoriaId == filtroRequest.Categoria);
            
        }
        
        if (filtroRequest.Fornecedor.HasValue)
        {
            produtosQuery = produtosQuery
                .Where(p => p.FornecedorId == filtroRequest.Fornecedor);
            
        }
     
        if (filtroRequest.VerificarValores())
        {
            switch (filtroRequest.PrecoCriterio)
            {
                case Criterio.MAIOR:
                    produtosQuery = produtosQuery.Where(p => p.ValorVenda > filtroRequest.Preco)
                        .OrderBy(p => p.ValorVenda);
                    break;
                case Criterio.MENOR:
                    produtosQuery = produtosQuery.Where(p => p.ValorVenda < filtroRequest.Preco)
                        .OrderBy(p => p.ValorVenda);
                    break;
                case Criterio.IGUAL:
                    produtosQuery = produtosQuery.Where(p => p.ValorVenda == filtroRequest.Preco)
                        .OrderBy(p => p.ValorVenda);
                    break;
            }
            
        }
        
        return produtosQuery.ToPagedList(filtroRequest.PageNumber,
            filtroRequest.PageSize);
    }
}
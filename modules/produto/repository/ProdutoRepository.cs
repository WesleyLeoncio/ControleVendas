using controle_vendas.infra.data;
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
     
        if (filtroRequest.VerificarValores())
        {
            switch (filtroRequest.PrecoCriterio)
            {
                case Criterio.MAIOR:
                    produtos = produtos.Where(p => p.ValorVenda > filtroRequest.Preco)
                        .OrderBy(p => p.ValorVenda);
                    break;
                case Criterio.MENOR:
                    produtos = produtos.Where(p => p.ValorVenda < filtroRequest.Preco)
                        .OrderBy(p => p.ValorVenda);
                    break;
                case Criterio.IGUAL:
                    produtos = produtos.Where(p => p.ValorVenda == filtroRequest.Preco)
                        .OrderBy(p => p.ValorVenda);
                    break;
            }
            
        }
        return produtos.ToPagedList(filtroRequest.PageNumber,
            filtroRequest.PageSize);
    }
}
using controle_vendas.modules.common.pagination;
using controle_vendas.modules.produto.models.entity;

namespace controle_vendas.modules.produto.models.response;

public class ProdutoPaginationResponse
{
    public IEnumerable<ProdutoResponse> Produtos { get; private set; }
    
    public MetaData<Produto> MetaData { get; private set; }

    public ProdutoPaginationResponse(IEnumerable<ProdutoResponse> produtos, MetaData<Produto> metaData)
    {
        Produtos = produtos;
        MetaData = metaData;
    }
}
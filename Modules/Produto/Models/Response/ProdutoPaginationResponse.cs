using ControleVendas.Modules.Common.Pagination;

namespace ControleVendas.Modules.Produto.Models.Response;

public class ProdutoPaginationResponse
{
    public IEnumerable<ProdutoResponse> Produtos { get; private set; }
    
    public MetaData<Entity.Produto> MetaData { get; private set; }

    public ProdutoPaginationResponse(IEnumerable<ProdutoResponse> produtos, MetaData<Entity.Produto> metaData)
    {
        Produtos = produtos;
        MetaData = metaData;
    }
}
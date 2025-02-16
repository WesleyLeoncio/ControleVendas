using ControleVendas.Modules.Common.Pagination;
using ControleVendas.Modules.Produto.Models.Entity;

namespace ControleVendas.Modules.Produto.Models.Response;

public class ProdutoPaginationResponse
{
    public IEnumerable<ProdutoResponse> Produtos { get; private set; }
    
    public MetaData<ProdutoEntity> MetaData { get; private set; }

    public ProdutoPaginationResponse(IEnumerable<ProdutoResponse> produtos, MetaData<ProdutoEntity> metaData)
    {
        Produtos = produtos;
        MetaData = metaData;
    }
}
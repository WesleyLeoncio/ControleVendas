using controle_vendas.modules.categoria.model.entity;
using controle_vendas.modules.common.pagination;

namespace controle_vendas.modules.categoria.model.response;

public class CategoriaPaginationProdutoResponse
{
    public IEnumerable<CategoriaProdutoResponse> Categorias { get; private set; }
    public MetaData<Categoria> MetaData { get; private set; }

    public CategoriaPaginationProdutoResponse(IEnumerable<CategoriaProdutoResponse> categorias, MetaData<Categoria> metaData)
    {
        Categorias = categorias;
        MetaData = metaData;
    }
}
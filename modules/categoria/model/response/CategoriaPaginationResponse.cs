using controle_vendas.modules.categoria.model.entity;
using controle_vendas.modules.common.pagination;

namespace controle_vendas.modules.categoria.model.response;

public class CategoriaPaginationResponse
{
    public IEnumerable<CategoriaResponse> Categorias { get; private set; }
    public MetaData<Categoria> MetaData { get; private set; }

    public CategoriaPaginationResponse(IEnumerable<CategoriaResponse> categorias, MetaData<Categoria> metaData)
    {
        Categorias = categorias;
        MetaData = metaData;
    }
}
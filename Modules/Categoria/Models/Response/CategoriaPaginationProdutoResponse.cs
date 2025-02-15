using ControleVendas.Modules.Common.Pagination;

namespace ControleVendas.Modules.Categoria.Models.Response;

public class CategoriaPaginationProdutoResponse
{
    public IEnumerable<CategoriaProdutoResponse> Categorias { get; private set; }
    public MetaData<Entity.Categoria> MetaData { get; private set; }

    public CategoriaPaginationProdutoResponse(IEnumerable<CategoriaProdutoResponse> categorias, MetaData<Entity.Categoria> metaData)
    {
        Categorias = categorias;
        MetaData = metaData;
    }
}
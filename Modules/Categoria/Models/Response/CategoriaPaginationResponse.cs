using ControleVendas.Modules.Common.Pagination;

namespace ControleVendas.Modules.Categoria.Models.Response;

public class CategoriaPaginationResponse
{
    public IEnumerable<CategoriaResponse> Categorias { get; private set; }
    public MetaData<Entity.Categoria> MetaData { get; private set; }

    public CategoriaPaginationResponse(IEnumerable<CategoriaResponse> categorias, MetaData<Entity.Categoria> metaData)
    {
        Categorias = categorias;
        MetaData = metaData;
    }
}
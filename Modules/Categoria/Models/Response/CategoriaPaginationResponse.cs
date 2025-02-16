using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Common.Pagination;

namespace ControleVendas.Modules.Categoria.Models.Response;

public class CategoriaPaginationResponse
{
    public IEnumerable<CategoriaResponse> Categorias { get; private set; }
    public MetaData<CategoriaEntity> MetaData { get; private set; }

    public CategoriaPaginationResponse(IEnumerable<CategoriaResponse> categorias, MetaData<CategoriaEntity> metaData)
    {
        Categorias = categorias;
        MetaData = metaData;
    }
}
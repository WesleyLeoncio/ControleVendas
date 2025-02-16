using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Common.Pagination;

namespace ControleVendas.Modules.Categoria.Models.Response;

public class CategoriaPaginationProdutoResponse
{
    public IEnumerable<CategoriaProdutoResponse> Categorias { get; private set; }
    public MetaData<CategoriaEntity> MetaData { get; private set; }

    public CategoriaPaginationProdutoResponse(IEnumerable<CategoriaProdutoResponse> categorias, MetaData<CategoriaEntity> metaData)
    {
        Categorias = categorias;
        MetaData = metaData;
    }
}
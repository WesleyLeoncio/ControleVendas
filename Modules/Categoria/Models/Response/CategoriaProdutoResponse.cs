using ControleVendas.Modules.Produto.Models.Response;

namespace ControleVendas.Modules.Categoria.Models.Response;

public record CategoriaProdutoResponse(
    int Id,
    string Nome,
    IEnumerable<ProdutoCategoriaResponse> Produtos
    );
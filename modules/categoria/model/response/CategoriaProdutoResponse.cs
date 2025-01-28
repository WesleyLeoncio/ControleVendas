using controle_vendas.modules.produto.models.entity;
using controle_vendas.modules.produto.models.response;

namespace controle_vendas.modules.categoria.model.response;

public record CategoriaProdutoResponse(
    int Id,
    string Nome,
    IEnumerable<ProdutoCategoriaResponse> Produtos
    );
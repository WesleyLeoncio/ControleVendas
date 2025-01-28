namespace controle_vendas.modules.produto.models.response;

public record ProdutoResponse(
    int Id,
    string Nome,
    decimal ValorVenda,
    int Estoque,
    int CategoriaId,
    int FornecedorId
);
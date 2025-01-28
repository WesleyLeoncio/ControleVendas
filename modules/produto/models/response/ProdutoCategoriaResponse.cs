namespace controle_vendas.modules.produto.models.response;

public record ProdutoCategoriaResponse(
    string Nome,
    decimal ValorVenda,
    int Estoque
);
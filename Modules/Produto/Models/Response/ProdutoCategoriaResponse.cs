namespace ControleVendas.Modules.Produto.Models.Response;

public record ProdutoCategoriaResponse(
    string Nome,
    decimal ValorVenda,
    string Descricao,
    int Estoque
);
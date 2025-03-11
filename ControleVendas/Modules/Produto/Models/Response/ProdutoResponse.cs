namespace ControleVendas.Modules.Produto.Models.Response;

public record ProdutoResponse(
    int Id,
    string Nome,
    decimal ValorVenda,
    string Descricao,
    int Estoque,
    int CategoriaId,
    int FornecedorId
);
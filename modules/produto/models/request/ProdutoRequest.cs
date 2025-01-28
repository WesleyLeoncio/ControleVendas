using System.ComponentModel.DataAnnotations;

namespace controle_vendas.modules.produto.models.request;

public record ProdutoRequest(
    
    [Required(ErrorMessage = "Campo Nome Obrigatorio!")] 
    [StringLength(80)] string? Nome,
    
    [Range(1,20000, ErrorMessage = "O Valor deve está dentro do Range (1 a 20000)")]
    decimal ValorCompra,
    
    [Range(1,20000, ErrorMessage = "O Valor deve está dentro do Range (1 a 20000)")]
    decimal ValorVenda,
    
    [Range(1,20000, ErrorMessage = "O Estoque deve está dentro do Range (1 a 20000)")]
    int Estoque,
    
    [Range(1,20000, ErrorMessage = "A CategoriaId deve ser informada e está dentro do Range (1 a 20000)")]
    int CategoriaId,
    
    [Range(1,20000, ErrorMessage = "A FornecedorId deve ser informada e está dentro do Range (1 a 20000)")]
    int FornecedorId
    
    
);
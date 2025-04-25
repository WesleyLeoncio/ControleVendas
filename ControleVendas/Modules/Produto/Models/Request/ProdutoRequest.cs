using System.ComponentModel.DataAnnotations;

namespace ControleVendas.Modules.Produto.Models.Request;

public record ProdutoRequest(
    
    [Required(ErrorMessage = "Campo Nome Obrigatório!")] 
    [StringLength(80)] string? Nome,
    
    [Range(1,20000, ErrorMessage = "O Valor deve está dentro do Range (1 a 20000)")]
    decimal ValorCompra,
    
    [Range(1,20000, ErrorMessage = "O Valor deve está dentro do Range (1 a 20000)")]
    decimal ValorVenda,
    
    [StringLength(280,ErrorMessage = "O Campo descrição deve ter no maximo 280 a caracter")]
    string Descricao,
    
    [Range(1,20000, ErrorMessage = "O Estoque deve está dentro do Range (1 a 20000)")]
    int Estoque,
    
    [Range(1,20000, ErrorMessage = "A CategoriaId deve ser informada e está dentro do Range (1 a 20000)")]
    int CategoriaId,
    
    [Range(1,20000, ErrorMessage = "A FornecedorId deve ser informada e está dentro do Range (1 a 20000)")]
    int FornecedorId
);
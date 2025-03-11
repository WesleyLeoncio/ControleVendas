namespace ControleVendas.Modules.Cliente.Models.Response;

public record ClienteResponse(
    int Id,
    string Nome,
    string Email,
    string Telefone,
    bool Ativo
);
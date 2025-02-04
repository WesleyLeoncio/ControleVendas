namespace controle_vendas.modules.cliente.model.response;

public record ClienteResponse(
    int Id,
    string Nome,
    string Email,
    string Telefone,
    bool Ativo
);
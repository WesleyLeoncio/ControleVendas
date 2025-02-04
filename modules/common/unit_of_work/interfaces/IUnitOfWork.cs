using controle_vendas.modules.categoria.repository.interfaces;
using controle_vendas.modules.cliente.repository.interfaces;
using controle_vendas.modules.fornecedor.repository.interfaces;
using controle_vendas.modules.produto.repository.interfaces;

namespace controle_vendas.modules.common.unit_of_work.interfaces;

public interface IUnitOfWork
{
    ICategoriaRepository CategoriaRepository { get; }
    IFornecedorRepository FornecedorRepository { get; }
    IProdutoRepository ProdutoRepository { get; }
    IClienteRepository ClienteRepository { get; }
    Task Commit();
}
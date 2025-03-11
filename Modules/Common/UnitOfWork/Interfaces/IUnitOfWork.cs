using ControleVendas.Modules.Categoria.Repository.Interfaces;
using ControleVendas.Modules.Cliente.Repository.Interfaces;
using ControleVendas.Modules.Fornecedor.Repository.Interfaces;
using ControleVendas.Modules.Pedido.Repository.Interfaces;
using ControleVendas.Modules.Produto.Repository.Interfaces;

namespace ControleVendas.Modules.Common.UnitOfWork.Interfaces;

public interface IUnitOfWork
{
    ICategoriaRepository CategoriaRepository { get; }
    IFornecedorRepository FornecedorRepository { get; }
    IProdutoRepository ProdutoRepository { get; }
    IClienteRepository ClienteRepository { get; }
    IPedidoRepository PedidoRepository { get; }
    Task Commit();
}
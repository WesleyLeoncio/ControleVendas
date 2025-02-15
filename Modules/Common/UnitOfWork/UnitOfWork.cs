using ControleVendas.Infra.Data;
using ControleVendas.Modules.Categoria.Repository;
using ControleVendas.Modules.Categoria.Repository.Interfaces;
using ControleVendas.Modules.Cliente.Repository;
using ControleVendas.Modules.Cliente.Repository.Interfaces;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Fornecedor.Repository;
using ControleVendas.Modules.Fornecedor.Repository.Interfaces;
using ControleVendas.Modules.Pedido.Repository;
using ControleVendas.Modules.Pedido.Repository.Interfaces;
using ControleVendas.Modules.Produto.Repository;
using ControleVendas.Modules.Produto.Repository.Interfaces;

namespace ControleVendas.Modules.Common.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    
    private ICategoriaRepository? _categoriaRepository;
    private IFornecedorRepository? _fornecedorRepository;
    private IProdutoRepository? _produtoRepository;
    private IClienteRepository? _clienteRepository;
    private IPedidoRepository? _pedidoRepository;
    private readonly AppDbConnectionContext _context;
    
    public UnitOfWork(AppDbConnectionContext context)
    {
        _context = context;
    }
    
    public ICategoriaRepository CategoriaRepository
    {
        get { return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context); }
    }
    
    public IFornecedorRepository FornecedorRepository
    {
        get { return _fornecedorRepository = _fornecedorRepository ?? new FornecedorRepository(_context); }
    }
    
    public IProdutoRepository ProdutoRepository
    {
        get { return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context); }
    }
    
    public IClienteRepository ClienteRepository
    {
        get { return _clienteRepository = _clienteRepository ?? new ClienteRepository(_context); }
    }

    public IPedidoRepository PedidoRepository
    {
        get { return _pedidoRepository = _pedidoRepository ?? new PedidoRepository(_context); }
    }


    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}
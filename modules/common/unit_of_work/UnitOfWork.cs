using controle_vendas.infra.data;
using controle_vendas.modules.categoria.repository;
using controle_vendas.modules.categoria.repository.interfaces;
using controle_vendas.modules.cliente.repository;
using controle_vendas.modules.cliente.repository.interfaces;
using controle_vendas.modules.common.unit_of_work.interfaces;
using controle_vendas.modules.fornecedor.repository;
using controle_vendas.modules.fornecedor.repository.interfaces;
using controle_vendas.modules.pedido.repository;
using controle_vendas.modules.pedido.repository.interfaces;
using controle_vendas.modules.produto.repository;
using controle_vendas.modules.produto.repository.interfaces;

namespace controle_vendas.modules.common.unit_of_work;

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
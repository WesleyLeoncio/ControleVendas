using ControleVendas.Infra.BackGraundTasks;
using ControleVendas.Infra.Exceptions.handle;
using ControleVendas.Infra.Exceptions.interfaces;
using ControleVendas.Modules.Categoria.Repository;
using ControleVendas.Modules.Categoria.Repository.Interfaces;
using ControleVendas.Modules.Categoria.Service;
using ControleVendas.Modules.Categoria.Service.Interfaces;
using ControleVendas.Modules.Cliente.Repository;
using ControleVendas.Modules.Cliente.Repository.Interfaces;
using ControleVendas.Modules.Cliente.Service;
using ControleVendas.Modules.Cliente.Service.Interfaces;
using ControleVendas.Modules.Common.Repository;
using ControleVendas.Modules.Common.Repository.Interfaces;
using ControleVendas.Modules.Common.UnitOfWork;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Fornecedor.Repository;
using ControleVendas.Modules.Fornecedor.Repository.Interfaces;
using ControleVendas.Modules.Fornecedor.Service;
using ControleVendas.Modules.Fornecedor.Service.Interfaces;
using ControleVendas.Modules.Pedido.Repository;
using ControleVendas.Modules.Pedido.Repository.Interfaces;
using ControleVendas.Modules.Pedido.Service;
using ControleVendas.Modules.Pedido.Service.Interfaces;
using ControleVendas.Modules.Produto.Repository;
using ControleVendas.Modules.Produto.Repository.Interfaces;
using ControleVendas.Modules.Produto.Service;
using ControleVendas.Modules.Produto.Service.Interfaces;
using ControleVendas.Modules.Token.Service;
using ControleVendas.Modules.Token.Service.Interfaces;
using ControleVendas.Modules.User.Service;
using ControleVendas.Modules.User.Service.Interfaces;

namespace ControleVendas.Infra.Config;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjections(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IFornecedorRepository, FornecedorRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IFornecedorService, FornecedorService>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPedidoService, PedidoService>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddTransient<IErrorResultTask, HandleNotFound>();
        services.AddTransient<IErrorResultTask, HandleKeyDuplication>();
        services.AddTransient<IErrorResultTask, HandleUnauthorized>();
        
        // Adiciona o serviço de background para verificar pedidos atrasados
        services.AddHostedService<PedidoAtrasadoService>();
        
        // Permite pega info do token nos services
        services.AddHttpContextAccessor();

    }
}
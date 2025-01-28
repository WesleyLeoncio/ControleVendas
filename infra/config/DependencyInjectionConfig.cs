using controle_vendas.infra.exceptions.handle;
using controle_vendas.infra.exceptions.interfaces;
using controle_vendas.modules.categoria.repository;
using controle_vendas.modules.categoria.repository.interfaces;
using controle_vendas.modules.categoria.service;
using controle_vendas.modules.categoria.service.interfaces;
using controle_vendas.modules.common.repository;
using controle_vendas.modules.common.repository.interfaces;
using controle_vendas.modules.common.unit_of_work;
using controle_vendas.modules.common.unit_of_work.interfaces;
using controle_vendas.modules.fornecedor.repository;
using controle_vendas.modules.fornecedor.repository.interfaces;
using controle_vendas.modules.fornecedor.service;
using controle_vendas.modules.fornecedor.service.interfaces;
using controle_vendas.modules.produto.repository;
using controle_vendas.modules.produto.repository.interfaces;
using controle_vendas.modules.produto.service;
using controle_vendas.modules.produto.service.interfaces;

namespace controle_vendas.infra.config;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjections(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IFornecedorRepository, FornecedorRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IFornecedorService, FornecedorService>();
        services.AddScoped<IProdutoService, ProdutoService>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddTransient<IErrorResultTask, HandleNotFound>();
        services.AddTransient<IErrorResultTask, HandleKeyDuplication>();
    }
}
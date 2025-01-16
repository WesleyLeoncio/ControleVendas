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

namespace controle_vendas.infra.config;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjections(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IFornecedorRepository, FornecedorRepository>();
        services.AddScoped<IFornecedorService, FornecedorService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IErrorResultTask, HandleNotFound>();
    }
}
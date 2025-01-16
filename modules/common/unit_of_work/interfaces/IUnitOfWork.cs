using controle_vendas.modules.categoria.repository.interfaces;
using controle_vendas.modules.fornecedor.repository.interfaces;

namespace controle_vendas.modules.common.unit_of_work.interfaces;

public interface IUnitOfWork
{
    ICategoriaRepository CategoriaRepository { get; }
    IFornecedorRepository FornecedorRepository { get; }
    Task Commit();
}
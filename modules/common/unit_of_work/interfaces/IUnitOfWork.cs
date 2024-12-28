using controle_vendas.modules.categoria.repository.interfaces;

namespace controle_vendas.modules.common.unit_of_work.interfaces;

public interface IUnitOfWork
{
    ICategoriaRepository CategoriaRepository { get; }
    Task Commit();
}
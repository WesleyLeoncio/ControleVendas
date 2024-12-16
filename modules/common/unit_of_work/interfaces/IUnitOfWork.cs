namespace controle_vendas.modules.common.unit_of_work.interfaces;

public interface IUnitOfWork
{
    Task Commit();
  
}
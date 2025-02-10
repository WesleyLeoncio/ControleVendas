using controle_vendas.infra.data;
using controle_vendas.modules.common.repository;
using controle_vendas.modules.pedido.models.entity;
using controle_vendas.modules.pedido.repository.interfaces;

namespace controle_vendas.modules.pedido.repository;

public class PedidoRepository : Repository<Pedido>, IPedidoRepository
{
    public PedidoRepository(AppDbConnectionContext context) : base(context)
    {
    }
}
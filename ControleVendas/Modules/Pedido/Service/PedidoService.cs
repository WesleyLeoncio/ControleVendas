using System.Security.Claims;
using AutoMapper;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Common.Pagination;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.ItemPedido.models.Entity;
using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Enums;
using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendas.Modules.Pedido.Models.Response;
using ControleVendas.Modules.Pedido.Service.Interfaces;
using ControleVendas.Modules.Produto.Models.Entity;
using X.PagedList;

namespace ControleVendas.Modules.Pedido.Service;

public class PedidoService : IPedidoService
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper; 
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PedidoService(IUnitOfWork uof, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _uof = uof;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
    

    public async Task RegistrarPedido(PedidoRequest pedidoRequest)
    {
        PedidoEntity pedidoEntity = _mapper.Map<PedidoEntity>(pedidoRequest);
        pedidoEntity.VendedorId = BuscarVendedorId();
        foreach (var item in pedidoRequest.Itens)
        {
            ProdutoEntity produtoEntity = await CheckProduto(item.ProdutoId);
            RetirarQuantidadeProduto(produtoEntity, item.Quantidade);

            ItemPedidoEntity itemPedidoEntity = new ItemPedidoEntity();
            itemPedidoEntity.ProdutoId = produtoEntity.Id;
            itemPedidoEntity.Quantidade = item.Quantidade;
            itemPedidoEntity.PrecoUnitario = produtoEntity.ValorVenda;
            itemPedidoEntity.CalcularLucroItemPedido(produtoEntity.ValorCompra);

            pedidoEntity.Itens.Add(itemPedidoEntity);
        }

        pedidoEntity.CalcularValorTotal();
        pedidoEntity = VerificarStatusPedido(pedidoEntity, pedidoRequest.Pagamento);
        _uof.PedidoRepository.Create(pedidoEntity);
        await _uof.Commit();
    }

    public async Task CancelarPedido(int pedidoId)
    {
        PedidoEntity pedidoEntity = await 
            _uof.PedidoRepository.GetPedidosIncludeItensPendentePorId(pedidoId);
        if (pedidoEntity.Status == StatusPedido.Cancelado) throw new ConflictException("Pedido já está cancelado");
        foreach (var item in pedidoEntity.Itens)
        {
            ProdutoEntity produtoEntity = await CheckProduto(item.ProdutoId);
            produtoEntity.Estoque += item.Quantidade;
            _uof.ProdutoRepository.Update(produtoEntity);
        }

        pedidoEntity.Status = StatusPedido.Cancelado;
        _uof.PedidoRepository.Update(pedidoEntity);
        await _uof.Commit();
    }

    public async Task RealizarPagamentoDePedido(PedidoPagamentoRequest pagamentoRequest)
    {
        PedidoEntity pedido = await CheckPedido(pagamentoRequest.IdPedido);
        if (pedido.Status != StatusPedido.Pago)
        {
            pedido = VerificarStatusPedido(pedido, pagamentoRequest.Pagamento);
            _uof.PedidoRepository.Update(pedido);
            await _uof.Commit();
        }
    }

    private static PedidoEntity VerificarStatusPedido(PedidoEntity pedido, decimal pagamento)
    {
        if (pagamento < 0)
            throw new ArgumentException("O valor do pagamento não pode ser negativo.");

        pedido.ValorPago += pagamento;
        if (pedido.ValorPago >= pedido.ValorTotal)
        {
            pedido.Status = StatusPedido.Pago;
        }
        else if (VerificarParcelasAtrasadas(pedido))
        {
            pedido.Status = StatusPedido.Atrasado;
        }
        else
        {
            pedido.Status = StatusPedido.Pendente;
        }

        return pedido;
    }

    public async Task<PedidoPaginationResponse> GetAllFilterPedidos(PedidoFiltroRequest filtro)
    {
        filtro.VendedorId = BuscarVendedorId();
        IPagedList<PedidoEntity> pedidos = await
            _uof.PedidoRepository.GetAllIncludeClienteFilterPageableAsync(filtro);

        PedidoPaginationResponse pedidoPg = new PedidoPaginationResponse(
            _mapper.Map<IEnumerable<PedidoResponse>>(pedidos),
            MetaData<PedidoEntity>.ToValue(pedidos));
        return pedidoPg;
    }

    public async Task VerificarPedidosAtrasados()
    {
        IEnumerable<PedidoEntity> pedidos = await _uof.PedidoRepository.GetAllPedidosStatusPendente();

        IEnumerable<PedidoEntity> pedidosAtrasados = pedidos
            .Where(VerificarParcelasAtrasadas)
            .Select(pedido =>
            {
                pedido.Status = StatusPedido.Atrasado;
                _uof.PedidoRepository.Update(pedido);
                return pedido;
            })
            .ToList();

        if (pedidosAtrasados.Any()) await _uof.Commit();
    }

    private static bool VerificarParcelasAtrasadas(PedidoEntity pedidoEntity)
    {
        if (pedidoEntity.NumeroParcelas <= 0 || pedidoEntity.ValorTotal <= 0) return false;
        if (pedidoEntity.ValorPago >= pedidoEntity.ValorTotal) return false;

        decimal valorParcela = pedidoEntity.ValorTotal / pedidoEntity.NumeroParcelas;
        int parcelasPagas = (int)Math.Floor(pedidoEntity.ValorPago / valorParcela);
        
        DateTime now = DateTime.Now;
        
        if (pedidoEntity.DataVenda == DateTime.MinValue) pedidoEntity.DataVenda = now;

        if (parcelasPagas >= pedidoEntity.NumeroParcelas) return false;

        return Enumerable.Range(parcelasPagas + 1, pedidoEntity.NumeroParcelas - parcelasPagas)
            .Select(i => pedidoEntity.DataVenda.AddMonths(i))
            .Any(dataVencimento => now > dataVencimento);
    }

    private async Task<ProdutoEntity> CheckProduto(int id)
    {
        return await _uof.ProdutoRepository.GetAsync(p => p.Id == id) ??
               throw new NotFoundException("Produto não encontrado!");
    }

    private void RetirarQuantidadeProduto(ProdutoEntity produtoEntity, int quantidade)
    {
        if (produtoEntity.Estoque < quantidade)
        {
            throw new NotFoundException($"Não a produtos o suficiente em estoque, "
                                        + $"Quantidade em estoque: {produtoEntity.Estoque}");
        }

        produtoEntity.Estoque = produtoEntity.Estoque - quantidade;
        _uof.ProdutoRepository.Update(produtoEntity);
    }
    
    private async Task<PedidoEntity> CheckPedido(int id)
    {
        return await _uof.PedidoRepository.GetAsync(p => p.Id == id) ??
               throw new NotFoundException("Pedido não encontrado!");
    }
    
    private string BuscarVendedorId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) throw new NotFoundException("Id Do Usuário não encontrado");
  
        return userId;
    }
}
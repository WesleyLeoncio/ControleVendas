using AutoMapper;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Common.Pagination;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Pedido.Models.Enums;
using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendas.Modules.Pedido.Models.Response;
using ControleVendas.Modules.Pedido.Service.Interfaces;
using X.PagedList;

namespace ControleVendas.Modules.Pedido.Service;

public class PedidoService : IPedidoService
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public PedidoService(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }


    public async Task RegistrarPedido(PedidoRequest pedidoRequest)
    {
        // if (user == null) throw new NotFoundException("Vendedor não encontrado!");
        Models.Entity.Pedido pedido = _mapper.Map<Models.Entity.Pedido>(pedidoRequest);
        pedido.VendedorId = "cadd2ea3-30bb-44a1-b409-ec65001fa6da";
        foreach (var item in pedidoRequest.Itens)
        {
            Produto.Models.Entity.Produto produto = await CheckProduto(item.ProdutoId);
            await RetirarQuantidadeProduto(produto, item.Quantidade);

            ItemPedido.models.Entity.ItemPedido itemPedido = new ItemPedido.models.Entity.ItemPedido();
            itemPedido.ProdutoId = produto.Id;
            itemPedido.Quantidade = item.Quantidade;
            itemPedido.PrecoUnitario = produto.ValorVenda;
            itemPedido.CalcularLucroItemPedido(produto.ValorCompra);

            pedido.Itens.Add(itemPedido);
        }

        pedido.CalcularValorTotal();
        _uof.PedidoRepository.Create(pedido);
        await _uof.Commit();
    }

    public async Task<PedidoPaginationResponse> GetAllFilterPedidos(PedidoFiltroRequest filtro)
    {
        filtro.VerdedorId = "cadd2ea3-30bb-44a1-b409-ec65001fa6da";
        IPagedList<Models.Entity.Pedido> pedidos = await
            _uof.PedidoRepository.GetAllIncludeClienteFilterPageableAsync(filtro);

        PedidoPaginationResponse pedidoPg = new PedidoPaginationResponse(
            _mapper.Map<IEnumerable<PedidoResponse>>(pedidos),
            MetaData<Models.Entity.Pedido>.ToValue(pedidos));
        return pedidoPg;
    }

    public async Task VerificarPedidosAtrasados()
    {
        IEnumerable<Models.Entity.Pedido> pedidos = await _uof.PedidoRepository.GetAllPedidosStatusPendente();

        IEnumerable<Models.Entity.Pedido> pedidosAtrasados = pedidos
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

    private static bool VerificarParcelasAtrasadas(Models.Entity.Pedido pedido)
    {
        if (pedido.NumeroParcelas <= 0 || pedido.ValorTotal <= 0) return false;
        if (pedido.ValorPago >= pedido.ValorTotal) return false;

        decimal valorParcela = pedido.ValorTotal / pedido.NumeroParcelas;
        int parcelasPagas = (int)(pedido.ValorPago / valorParcela);
        DateTime now = DateTime.Now;

        if (parcelasPagas >= pedido.NumeroParcelas) return false;

        return Enumerable.Range(parcelasPagas + 1, pedido.NumeroParcelas - parcelasPagas)
            .Select(i => pedido.DataVenda.AddMonths(i))
            .Any(dataVencimento => now > dataVencimento);
    }

    private async Task<Produto.Models.Entity.Produto> CheckProduto(int id)
    {
        return await _uof.ProdutoRepository.GetAsync(p => p.Id == id) ??
               throw new NotFoundException("Produto não encontrado!");
    }

    private async Task RetirarQuantidadeProduto(Produto.Models.Entity.Produto produto, int quantidade)
    {
        if (produto.Estoque < quantidade)
        {
            throw new NotFoundException($"Não a produtos o suficiente em estoque, "
                                        + $"Quandidade em estoque: {produto.Estoque}");
        }

        produto.Estoque = produto.Estoque - quantidade;
        _uof.ProdutoRepository.Update(produto);
        await _uof.Commit();
    }
}
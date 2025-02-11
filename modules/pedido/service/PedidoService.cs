using AutoMapper;
using controle_vendas.infra.exceptions.custom;
using controle_vendas.modules.common.unit_of_work.interfaces;
using controle_vendas.modules.item_pedido.models.entity;
using controle_vendas.modules.item_pedido.models.request;
using controle_vendas.modules.pedido.models.entity;
using controle_vendas.modules.pedido.models.request;
using controle_vendas.modules.pedido.service.interfaces;
using controle_vendas.modules.produto.models.entity;

namespace controle_vendas.modules.pedido.service;

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
        //if (user == null) throw new NotFoundException("Vendedor não encontrado!");
        Pedido pedido =  _mapper.Map<Pedido>(pedidoRequest);
        pedido.VendedorId = "cadd2ea3-30bb-44a1-b409-ec65001fa6da";
        foreach (var item in pedidoRequest.Itens)
        {
            await ValidarProduto(item);
            
            ItemPedido itemPedido = new ItemPedido
            {
                ProdutoId = item.ProdutoId,
                Quantidade = item.Quantidade,
            };
           pedido.Itens.Add(itemPedido);
        }
        
        Pedido entity = _uof.PedidoRepository.Create(pedido);
        await _uof.Commit();
        Console.WriteLine(entity);
    }
    
    private async Task<Produto> CheckProduto(int id)
    {
        return await _uof.ProdutoRepository.GetAsync(p => p.Id == id) ??
               throw new NotFoundException("Produto não encontrado!");
    }

    private async Task ValidarProduto(ItemPedidoRequest item)
    {
        Produto produto = await CheckProduto(item.ProdutoId);
        await RetirarQuantidadeProduto(produto, item.Quantidade);
        
    }

    private async Task RetirarQuantidadeProduto(Produto produto, int quantidade)
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
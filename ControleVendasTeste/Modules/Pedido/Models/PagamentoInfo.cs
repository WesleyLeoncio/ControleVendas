using ControleVendas.Modules.Pedido.Models.Enums;

namespace ControleVendasTeste.Modules.Pedido.Models;

public class PagamentoInfo
{
    public MetodoPagamento FormaPagamento { get; set; }
    public int NumeroParcelas { get; set; }
    public decimal Desconto { get; set; }
    public decimal ValorPago { get; set; }
}
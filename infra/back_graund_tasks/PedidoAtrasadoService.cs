using controle_vendas.modules.pedido.service.interfaces;

namespace controle_vendas.Infra.back_graund_tasks;

public class PedidoAtrasadoService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PedidoAtrasadoService> _logger;
    private static readonly TimeSpan IntervaloExecucao = TimeSpan.FromDays(1);

    public PedidoAtrasadoService(IServiceProvider serviceProvider, ILogger<PedidoAtrasadoService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("🔄 Serviço de verificação de pedidos atrasados iniciado.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var pedidoService = scope.ServiceProvider.GetRequiredService<IPedidoService>();
                await pedidoService.VerificarPedidosAtrasados();

                _logger.LogInformation("✅ Verificação de pedidos atrasados concluída.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Erro ao verificar pedidos atrasados.");
            }

            await Task.Delay(IntervaloExecucao, stoppingToken);
        }
    }
}
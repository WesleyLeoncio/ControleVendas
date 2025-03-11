using System.Threading.RateLimiting;

namespace ControleVendas.Infra.Config;

public static class RateLimiterConfig
{
    public static void AddRateLimiterGlobal(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpcontext =>
                RateLimitPartition.GetFixedWindowLimiter<string>(
                    partitionKey: httpcontext.User.Identity?.Name ?? httpcontext.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 2,
                        QueueLimit = 2,
                        Window = TimeSpan.FromSeconds(5),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                    }
                )
            );
        });
    }
}
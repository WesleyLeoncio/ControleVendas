namespace ControleVendas.Infra.Config;

public static class AuthorizationConfig
{
    public static void AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Master", policy => policy.RequireRole("Master"));
            options.AddPolicy("Vendedor", policy => policy.RequireRole("Vendedor"));
        });
    }
}
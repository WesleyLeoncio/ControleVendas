namespace controle_vendas.infra.config;

public static class AuthorizationConfig
{
    public static void AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("MASTER", policy => policy.RequireRole("MASTER"));
            options.AddPolicy("VENDEDOR", policy => policy.RequireRole("VENDEDOR"));
        });
    }
}
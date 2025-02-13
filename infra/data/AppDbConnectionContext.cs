using controle_vendas.modules.categoria.model.entity;
using controle_vendas.modules.cliente.model.entity;
using controle_vendas.modules.fornecedor.model.entity;
using controle_vendas.modules.item_pedido.models.entity;
using controle_vendas.modules.pedido.models.entity;
using controle_vendas.modules.produto.models.entity;
using controle_vendas.modules.user.models.entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace controle_vendas.infra.data;

public class AppDbConnectionContext(DbContextOptions options) : 
    IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Categoria> CategoriaBd { get; set; }
    public DbSet<Fornecedor> FornecedorBd { get; set; }
    public DbSet<Produto> ProdutoBd { get; set; }
    public DbSet<Cliente> ClienteBd { get; set; }
    public DbSet<Pedido> PedidoBd { get; set; }
    public DbSet<ItemPedido> ItemPedidoBd { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Pedido>()
            .Property(p => p.Status)
            .HasConversion<string>(); // Salva como texto no banco
        
        builder.Entity<Pedido>()
            .Property(p => p.FormaPagamento)
            .HasConversion<string>(); // Salva como texto no banco
    }
    
}
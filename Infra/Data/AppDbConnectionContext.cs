using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Fornecedor.Models.Entity;
using ControleVendas.Modules.ItemPedido.models.Entity;
using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.User.Models.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ControleVendas.Infra.Data;

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
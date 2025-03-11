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
    IdentityDbContext<ApplicationUserEntity>(options)
{
    public DbSet<CategoriaEntity> CategoriaBd { get; set; }
    public DbSet<FornecedorEntity> FornecedorBd { get; set; }
    public DbSet<ProdutoEntity> ProdutoBd { get; set; }
    public DbSet<ClienteEntity> ClienteBd { get; set; }
    public DbSet<PedidoEntity> PedidoBd { get; set; }
    public DbSet<ItemPedidoEntity> ItemPedidoBd { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<PedidoEntity>()
            .Property(p => p.Status)
            .HasConversion<string>(); // Salva como texto no banco
        
        builder.Entity<PedidoEntity>()
            .Property(p => p.FormaPagamento)
            .HasConversion<string>(); // Salva como texto no banco
        
    }
    
}
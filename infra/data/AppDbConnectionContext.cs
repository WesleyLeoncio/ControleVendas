using controle_vendas.modules.user.models.entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace controle_vendas.infra.data;

public class AppDbConnectionContext(DbContextOptions options) : 
    IdentityDbContext<ApplicationUser>(options)
{
    // public DbSet<Categoria>? CategoriaBd { get; set; }
    //
    // public DbSet<Produto>? ProdutoBd { get; set; }
}
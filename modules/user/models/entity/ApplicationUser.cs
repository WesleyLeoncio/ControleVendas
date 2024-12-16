using Microsoft.AspNetCore.Identity;

namespace controle_vendas.modules.user.models.entity;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpiryTime { get; set; }
}
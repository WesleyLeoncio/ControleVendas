using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace controle_vendas.modules.user.models.entity;

public class ApplicationUser : IdentityUser
{   
    [StringLength(256)]
    public string? RefreshToken { get; set; }
    
    [Required]
    [StringLength(256)]
    public string? FullName { get; set; }
    
    public DateTime RefreshTokenExpiryTime { get; set; }
}
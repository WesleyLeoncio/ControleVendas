using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ControleVendas.Modules.User.Models.Entity;

public class ApplicationUser : IdentityUser
{   
    [StringLength(256)]
    public string? RefreshToken { get; set; }
    
    [Required]
    [StringLength(256)]
    public string? FullName { get; set; }
    
    public DateTime RefreshTokenExpiryTime { get; set; }
}
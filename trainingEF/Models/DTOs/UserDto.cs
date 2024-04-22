using Microsoft.AspNetCore.Identity;

namespace trainingEF.Models.DTOs;

public class UserDto : IdentityUser
{
    public string? Address { get; set; }
    public ICollection<Order>? Orders { get; set; }
}

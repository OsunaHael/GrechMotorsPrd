using GrechMotorsPrd.Shared.Models;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public UserModel User { get; set; }
}
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public Doctor? Doctor { get; set; }

    public Patient? Patient { get; set; }
}
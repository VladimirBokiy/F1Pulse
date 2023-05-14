using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace F1Pulse.Data;

public class ApplicationUser : IdentityUser
{
    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [Display(Name = "Last name")]
    public string LastName { get; set; }
}
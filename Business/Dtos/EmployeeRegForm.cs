using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace Business.Dtos;

public class EmployeeRegForm
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Phone]
    public string? Phone { get; set; }

    public int RoleId { get; set; }

  

}

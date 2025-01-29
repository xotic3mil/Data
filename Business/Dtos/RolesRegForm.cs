using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class RolesRegForm
{
    [Required]
    public string RoleName { get; set; } = null!;
}

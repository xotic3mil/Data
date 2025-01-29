
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class StatusTypeRegForm
{
    [Required]
    public string Status { get; set; } = null!;
}

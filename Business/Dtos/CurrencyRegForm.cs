using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class CurrencyRegForm
{
    [Required]
    public string Currency { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ServiceRegForm
{
    [Required]
    public string ServiceName { get; set; } = null!;
    public decimal Price { get; set; }
    public int UnitId { get; set; }
    public int CurrencyId { get; set; }



}

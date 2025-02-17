using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ServiceRegForm
{
    [Required]
    public string ServiceName { get; set; } = null!;
    public string ServiceDescription { get; set; } = null!;
    public decimal StartupPrice { get; set; }
    public decimal Price { get; set; }
    public int UnitId { get; set; }
    public int CurrencyId { get; set; }



}

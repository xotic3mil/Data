using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("Services")]
public class ServiceEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string ServiceName { get; set; } = null!;

    [Required]
    [StringLength(800)]
    public string ServiceDescription { get; set; } = null!;

    [Required]
    public decimal StartupPrice { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int UnitId { get; set; }
    [ForeignKey(nameof(UnitId))]
    public UnitEntity? Units { get; set; }

    [Required]
    public int CurrencyId { get; set; }
    [ForeignKey(nameof(CurrencyId))]
    public CurrencyEntity? Currencies { get; set; }

    // To see which Projects are using this service
    public ICollection<ProjectEntity>? Projects { get; set; }
}

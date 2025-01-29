using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("Services")]
public class ServiceEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string ServiceName { get; set; } = null!;
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

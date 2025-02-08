using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("StatusTypes")]
public class StatusTypesEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Status { get; set; } = null!;

    // Navigation (optional): If you want to see which Projects are using this status
    public ICollection<ProjectEntity>? Projects { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("statusType")]
public class StatusTypesEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Status { get; set; } = default!;

    // Navigation (optional): If you want to see which Projects are using this status
    public ICollection<ProjectEntity>? Projects { get; set; }
}

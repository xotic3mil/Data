using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("Customers")]
public class CustomerEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string CompanyName { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(CustomerContactPersonId))]
    public int CustomerContactPersonId { get; set; }

    // Navigation to CustomerContactPerson 
    public CustomerContactPersonEntity ContactPerson { get; set; } = null!;

    // Navigation (optional): If you want to see which Projects belong to this Customer
    public ICollection<ProjectEntity>? Projects { get; set; }

}

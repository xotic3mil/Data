using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("customer")]
public class CustomerEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Name { get; set; }

    [ForeignKey(nameof(ContactPerson))]
    public int? ContactPersonId { get; set; }

    // Navigation to ContactPerson
    public ContactPersonEntity? ContactPerson { get; set; }

    // Navigation (optional): If you want to see which Projects belong to this Customer
    public ICollection<ProjectEntity>? Projects { get; set; }

}

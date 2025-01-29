using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("CustomerContacts")]
public class CustomerContactPersonsEntity
{
    [Key]
    public int CustomerId { get; set; }
    public int ContactPersonId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public CustomerEntity? Customer { get; set; }

    [ForeignKey(nameof(ContactPersonId))]
    public ContactPersonEntity? ContactPersons { get; set; }
}
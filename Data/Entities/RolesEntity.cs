using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("roles")]
public class RolesEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;


    // Navigation (optional): List of employees with this role
    public ICollection<EmployeeEntity>? Employees { get; set; }


}

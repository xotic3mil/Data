using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("Roles")]
public class RolesEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;


    // Navigation (optional): List of employees with roles
    public ICollection<EmployeeEntity>? Employees { get; set; }


}

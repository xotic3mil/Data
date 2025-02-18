using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("Employees")]
[Index(nameof(Email), IsUnique = true)]
public class EmployeeEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Phone]
    public string Phone { get; set; } = null!;

    public DateOnly? ContractStartDate { get; set; }

    [Required]
    public int RoleId { get; set; }
    [ForeignKey(nameof(RoleId))]
    public RolesEntity Role { get; set; } = null!;

    public ICollection<ProjectEntity>? Projects { get; set; }

    public ICollection<ServiceEntity> Services { get; set; } = new List<ServiceEntity>();

}



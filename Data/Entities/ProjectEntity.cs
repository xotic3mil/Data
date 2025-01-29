using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("project")]
public class ProjectEntity

{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int ProjectNumber { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    [Required]
    [ForeignKey(nameof(StatusId))]
    public int StatusId { get; set; }
    public StatusTypesEntity? Status { get; set; }

    [Required]
    [ForeignKey(nameof(Employee))]
    public int EmployeeId { get; set; }
    public EmployeeEntity? Employee { get; set; }

    [Required]
    [ForeignKey(nameof(Service))]
    public int ServiceId { get; set; }
    public ServiceEntity? Service { get; set; }

    [Required]
    [ForeignKey(nameof(Customer))]
    public int CustomerId { get; set; }
    public CustomerEntity? Customer { get; set; }


   
    
    
    
}





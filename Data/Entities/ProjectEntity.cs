﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("Projects")]
public class ProjectEntity

{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public Guid ProjectNumber { get; set; } = Guid.NewGuid();
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [StringLength(800)]
    public string Description { get; set; } = null!;

    [Required]
    public DateOnly StartDate { get; set; }
    [Required]
    public DateOnly EndDate { get; set; }

    [Required]
    public string Priority { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(StatusId))]
    public int StatusId { get; set; }
    public StatusTypesEntity Status { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Employee))]
    public int EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Service))]
    public int ServiceId { get; set; }
    public ServiceEntity Service { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Customer))]
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;








}





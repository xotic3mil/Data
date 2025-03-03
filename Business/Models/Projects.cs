﻿namespace Business.Models;

public class Projects
{
    public int Id { get; set; }
    public Guid ProjectNumber { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string Priority { get; set; } = null!;
    public int StatusId { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public int ServiceId { get; set; }
    public StatusTypes Status { get; set; } = null!;
    public Customers Customers { get; set; } = null!;
    public Employee Employee { get; set; } = null!;
    public Service Service { get; set; } = null!;

}

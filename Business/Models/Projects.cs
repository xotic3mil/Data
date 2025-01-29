namespace Business.Models;

public class Projects
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string StartDate { get; set; } = null!;
    public string EndDate { get; set; } = null!;
    public int StatusId { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public int ServiceId { get; set; }

}

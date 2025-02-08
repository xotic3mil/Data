namespace Business.Models;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;

    public DateOnly? ContractStartDate { get; set; }
    public int RoleId { get; set; } 
    public Roles Roles { get; set; } = null!;

}

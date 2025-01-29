namespace Business.Models;

public class Customers
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
}

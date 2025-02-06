using Data.Entities;

namespace Business.Models;

public class Customers
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = null!;
    // public int CustomerContactPersonId { get; set; }
    public CustomerContactPerson CustomerContactPerson { get; set; }
}

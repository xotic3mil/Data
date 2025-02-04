﻿namespace Business.Models;

public class Service
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = null!;
    public decimal Price { get; set; }
    public int UnitId { get; set; }
    public int CurrencyId {get; set;}
    public Units Units { get; set; } = null!;
    public Currencies Currencies { get; set; } = null!;

}

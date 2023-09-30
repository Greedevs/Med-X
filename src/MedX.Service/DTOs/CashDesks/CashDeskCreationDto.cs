namespace MedX.Service.DTOs.CashDesks;

public class CashDeskCreationDto
{
    public string Description { get; set; }
    public decimal Balance { get; set; }
    public string AccountNumber { get; set; }
    public bool IsIncome { get; set; }
}
namespace Backend.Shared.Domain;

public class Transaction
{
    public Guid Id { get; set; }
    public string IncomeType { get; set; } = string.Empty;
    public int Amount { get; set; }
    public DateTime DateTime { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public User? User { get; set; }
}

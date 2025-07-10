using System.Text.Json.Serialization;

namespace Backend.Shared.Domain;

public class Transaction
{
    public Transaction() { }

    public Guid Id { get; set; }
    public string IncomeType { get; set; } = string.Empty;
    public int Amount { get; set; }
    public DateTime DateTime { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    private Transaction(string incomeType, int amount, string description, Guid userId)
    {
        IncomeType = incomeType;
        Amount = amount;
        Description = description;
        DateTime = DateTime.UtcNow;
        UserId = userId;
    }

    public void UpdateTrasaction(string? incomeType, int? amount, string? description)
    {
        IncomeType = incomeType ?? IncomeType;
        Amount = amount ?? Amount;
        Description = description ?? Description;

    }

    public static Transaction CreateTransaction(string incomeType, int amount, string description, Guid userId)
         => new(incomeType, amount, description, userId);

}

using Backend.Features.GetTransaction;
using Backend.Shared;
using Backend.Shared.Domain;

namespace Backend.Features.TransactionFeature;

public class TransactionMapper(DateTimeConverter timeConverter)
{

    public TransactionDto MapToDto(Transaction transaction)
            => new(
                Id: transaction.Id,
                 IncomeType: transaction.IncomeType,
                 Amount: transaction.Amount,
                 DateTime: timeConverter.ConvertToPersianCalender(transaction.DateTime),
                 Description: transaction.Description);

    public List<TransactionDto> MapToDto(List<Transaction> transactions)
    {
        var list = new List<TransactionDto>();
        foreach (var transaction in transactions)
        {
            var dto = new TransactionDto
            (
               Id: transaction.Id,
                IncomeType: transaction.IncomeType,
                Amount: transaction.Amount,
                DateTime: timeConverter.ConvertToPersianCalender(transaction.DateTime),
                Description: transaction.Description
            );
            list.Add(dto);
        }
        return list;
    }
}

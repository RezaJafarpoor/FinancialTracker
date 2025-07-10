using Backend.Features.GetTransaction;
using Backend.Shared;
using Backend.Shared.Domain;

namespace Backend.Features.TransactionFeature;

public class TransactionMapper(DateTimeConverter timeConverter)
{

    public TransactionDto MapToDto(Transaction transaction)
    {
        var dateAndTime = timeConverter.ConvertToPersianCalender(transaction.DateTime);
        return new(
         Id: transaction.Id,
         IncomeType: transaction.IncomeType,
         Amount: transaction.Amount,
         Date: dateAndTime.Date,
         Time: dateAndTime.Time,
         Description: transaction.Description);
    }

    public List<TransactionDto> MapToDto(List<Transaction> transactions)
    {
        var list = new List<TransactionDto>();
        foreach (var transaction in transactions)
        {
            var dateAndTime = timeConverter.ConvertToPersianCalender(transaction.DateTime);
            var dto = new TransactionDto
            (
                Id: transaction.Id,
                IncomeType: transaction.IncomeType,
                Amount: transaction.Amount,
                Date: dateAndTime.Date,
                Time: dateAndTime.Time,
                Description: transaction.Description
            );
            list.Add(dto);
        }
        return list;
    }
}

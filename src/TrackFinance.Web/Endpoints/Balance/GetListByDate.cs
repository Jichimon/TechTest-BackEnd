using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using TrackFinance.Core.Interfaces;
using TrackFinance.Core.Services;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.Balance;

public class GetListByDate : EndpointBaseAsync
    .WithRequest<BalanceByDateRequest>
    .WithActionResult<BalanceListResponse>
{

  private readonly ITransactionFinanceService _transactionService;

  public GetListByDate(ITransactionFinanceService transactionService)
  {
    _transactionService = transactionService;
  }


  public override async Task<ActionResult<BalanceListResponse>> HandleAsync(BalanceByDateRequest request, CancellationToken cancellationToken = default)
  {
    var transactions = await _transactionService.GetTransactionsByDateAsync(request.UserId, request.StartDate, request.EndDate, request.DateType, cancellationToken);

    if (transactions.Status != ResultStatus.Ok) return BadRequest(transactions.ValidationErrors);

    return Ok(new BalanceListResponse
    {
      ExpensesTransaction = GetTransactionsRecords(transactions.Value, TransactionType.Expense),
      IncomesTransaction = GetTransactionsRecords(transactions.Value, TransactionType.Income)
    });
  }


  private static List<TransactionRecord>? GetTransactionsRecords(List<TransactionDataDto> transactions, TransactionType transactionType)
  {
    return new List<TransactionRecord>(transactions.Where(x => x.TransactionType == transactionType)
                       .Select(item => new TransactionRecord(
                        item.Date,
                        item.DayOfWeek,
                        item.Day,
                        item.TotalAmount,
                        item.TransactionDescriptionType,
                        item.Week,
                        item.Year,
                        item.Month
                       )));
  }

}

using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using TrackFinance.Core.Interfaces;

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


  public override Task<ActionResult<BalanceListResponse>> HandleAsync(BalanceByDateRequest request, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}

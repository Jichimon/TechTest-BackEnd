using System.Net.Mime;
using Ardalis.ApiEndpoints;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.Interfaces;


namespace TrackFinance.Web.Endpoints.Historical;

public class GetHistoricalRecordByUser : EndpointBaseAsync
  .WithRequest<GetHistoricalRecordByUserRequest>
  .WithActionResult<GetHistoricalRecordByUserResponse>
{
  private readonly IHistoricalRecordService _historicalRecordService;

  public GetHistoricalRecordByUser(IHistoricalRecordService historicalRecordService)
  {
    _historicalRecordService = historicalRecordService;
  }

  [Produces(MediaTypeNames.Application.Json)]
  [HttpGet(GetHistoricalRecordByUserRequest.Route)]
  [SwaggerOperation(
      Summary = "Get Historical Records by user",
      Description = "Get Historical Records by userId",
      OperationId = "HistoricalRecords.GetHistoricalRecordByUser",
      Tags = new[] { "HistoricalRecordsEndpoints" })
  ]
  public override async Task<ActionResult<GetHistoricalRecordByUserResponse>> HandleAsync([FromRoute] GetHistoricalRecordByUserRequest request, CancellationToken cancellationToken = default) {
    var response = await _historicalRecordService.GetTransactionsByDateAsync(request.UserId, request.StartDate, request.EndDate);

    if (response.Status == ResultStatus.Invalid) return BadRequest(response.ValidationErrors);
    if (response.Status == ResultStatus.NotFound) return NotFound();

    if (response.Status == ResultStatus.Ok)
    {
      var records = response.Value.Select(expense => new HistoricalRecord(
                            description: expense.Description,
                            transactionDescriptionType: expense.TransactionDescriptionType,
                            amount: expense.Amount,
                            expenseDate: expense.ExpenseDate,
                            transactionType: expense.TransactionType))
                           .OrderBy(d => d.expenseDate)
                           .ToList();
      
      return Ok(new GetHistoricalRecordByUserResponse
      {
        HistoricalRecord = records
      });
    }

    return Ok(response);
  }
}

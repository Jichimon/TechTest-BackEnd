using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.SharedKernel.Interfaces;
using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Expenses;

public class Update : EndpointBaseAsync
    .WithRequest<UpdateExpenseRequest>
    .WithActionResult<UpdateExpenseResponse>
{

  private readonly IRepository<Transaction> _repository;

  public Update(IRepository<Transaction> repository)
  {
    _repository = repository;
  }

  [HttpPut(UpdateIncomeRequest.Route)]
  [Produces("application/json")]
  [SwaggerOperation(
     Summary = "Updates a incomes",
     Description = "Updates a incomes",
     OperationId = "Income.Update",
     Tags = new[] { "IncomesEndpoints" })
  ]
  public override async Task<ActionResult<UpdateExpenseResponse>> HandleAsync(UpdateExpenseRequest request, CancellationToken cancellationToken = default)
  {
    var existingExpenses = await _repository.GetByIdAsync(request.Id, cancellationToken);

    if (existingExpenses == null)
    {
      return NotFound();
    }

    existingExpenses.UpdateValue(request.Description, request.Amount, request.ExpenseType, request.ExpenseDate, request.UserId, TransactionType.Income);

    await _repository.UpdateAsync(existingExpenses, cancellationToken);

    var response = new UpdateExpenseResponse(
        expenseRecord: new ExpenseRecord(
          existingExpenses.Id,
          existingExpenses.Description,
          existingExpenses.Amount,
          existingExpenses.TransactionDescriptionType,
          existingExpenses.ExpenseDate,
          existingExpenses.UserId,
          existingExpenses.TransactionType));

    return Ok(response);
  }

}

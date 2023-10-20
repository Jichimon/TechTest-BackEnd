using FluentValidation;

namespace TrackFinance.Web.Endpoints.Expenses;

public class UpdateExpenseValidator : AbstractValidator<UpdateExpenseRequest>
{
  public UpdateExpenseValidator()
  {
    RuleFor(expense => expense.Amount).GreaterThan(0);
    RuleFor(expense => expense.Description).NotEmpty().NotNull();
  }
}

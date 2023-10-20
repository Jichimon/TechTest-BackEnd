using FluentValidation;

namespace TrackFinance.Web.Endpoints.Balance;

public class BalanceByDateValidator : AbstractValidator<BalanceByDateRequest>
{

  public BalanceByDateValidator() 
  {
    RuleFor(expense => expense.UserId).GreaterThan(0);
    RuleFor(expense => expense.EndDate).GreaterThanOrEqualTo(expense => expense.StartDate);
    RuleFor(expense => expense.StartDate).LessThanOrEqualTo(expense => expense.EndDate);
  }
}

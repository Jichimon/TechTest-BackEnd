using System.ComponentModel.DataAnnotations;

namespace TrackFinance.Web.Endpoints.Expenses;

public class GetExpenseByIdRequest
{
  public const string Route = "/Expenses/{ExpenseId:int}";
  public static string BuildRoute(int expenseId) => Route.Replace("{ExpenseId:int}", expenseId.ToString());

  [Required]
  public int ExpenseId { get; set; }
}

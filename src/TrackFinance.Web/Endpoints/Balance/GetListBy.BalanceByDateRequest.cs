using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.Balance;

public class BalanceByDateRequest
{
  public const string Route = "/Balance/{UserId}/{DateType}";

  [FromRoute]
  public int UserId { get; set; }

  [FromRoute]
  public DateType DateType { get; set; }

  [FromQuery(Name = "startDate")]
  [Required]
  public DateTime StartDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("d"));

  [FromQuery(Name = "endDate")]
  [Required]
  public DateTime EndDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("d"));

}

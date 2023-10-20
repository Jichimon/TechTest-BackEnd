﻿using Microsoft.AspNetCore.Mvc;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.Balance;

public class BalanceByDateRequest
{
  public const string Route = "/Balance/{UserId}/{TransactionType}";

  [FromRoute]
  public int UserId { get; set; }

  [FromRoute]
  public TransactionType TransactionType { get; set; }

  [FromQuery(Name = "startDate")]
  public DateTime StartDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("d"));
  
  [FromQuery(Name = "endDate")]
  public DateTime EndDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("d"));
}

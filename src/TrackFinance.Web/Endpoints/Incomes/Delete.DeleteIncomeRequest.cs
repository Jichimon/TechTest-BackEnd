﻿namespace TrackFinance.Web.Endpoints.Incomes;

public class DeleteIncomeRequest
{
  public const string Route = "/Incomes/{IncomeId:int}";
  public static string BuildRoute(int incomeId) => Route.Replace("{IncomeId:int}", incomeId.ToString());
  public int IncomeId { get; set; }
}

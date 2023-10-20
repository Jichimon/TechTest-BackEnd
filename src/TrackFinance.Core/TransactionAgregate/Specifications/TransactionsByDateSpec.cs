using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using MediatR;

namespace TrackFinance.Core.TransactionAgregate.Specifications;
public class TransactionsByDateSpec : Specification<Transaction>
{
  public TransactionsByDateSpec(int userId, DateTime startDate, DateTime endDate)
  {
    Query
      .Where(transaction => transaction.UserId == userId)
      .Where(transaction => transaction.ExpenseDate >= startDate && transaction.ExpenseDate <= endDate)
      .OrderBy(transaction => transaction.ExpenseDate);
  }
}

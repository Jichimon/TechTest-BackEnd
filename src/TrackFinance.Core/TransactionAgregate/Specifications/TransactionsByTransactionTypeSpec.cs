using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using TrackFinance.Core.TransactionAgregate.Enum;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TrackFinance.Core.TransactionAgregate.Specifications;
public class TransactionsByTransactionTypeSpec : Specification<Transaction>
{
  public TransactionsByTransactionTypeSpec(TransactionType transactionType)
  {
    Query
      .Where(transaction => transaction.TransactionType == transactionType);
  }
}

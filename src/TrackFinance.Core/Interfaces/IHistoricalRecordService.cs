using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using TrackFinance.Core.TransactionAgregate;

namespace TrackFinance.Core.Interfaces;
public interface IHistoricalRecordService
{
  public Task<Result<List<Transaction>>> GetTransactionsByDateAsync(int userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
}

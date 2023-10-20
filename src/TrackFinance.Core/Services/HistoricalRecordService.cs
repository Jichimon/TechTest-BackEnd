using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Microsoft.Extensions.Caching.Memory;
using TrackFinance.Core.Interfaces;
using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Specifications;
using TrackFinance.SharedKernel.Interfaces;

namespace TrackFinance.Core.Services;
public class HistoricalRecordService : IHistoricalRecordService
{
  private readonly IRepository<Transaction> _repository;
  private readonly IMemoryCache _memoryCache;

  public HistoricalRecordService(IRepository<Transaction> repository, IMemoryCache memoryCache)
  {
    _repository = repository;
    _memoryCache = memoryCache;
  }

  public async Task<Result<List<Transaction>>> GetTransactionsByDateAsync(int userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
  {
    var itemsByDate = await LoadListAsync(userId, startDate, endDate, cancellationToken);

    if (!itemsByDate.Any()) return Result<List<Transaction>>.NotFound();

    return itemsByDate;
  }


  private async Task<List<Transaction>> LoadListAsync(int userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
  {
    var cacheKey = "$_userId_" + userId.ToString();
    
    var data = _memoryCache.Get<List<Transaction>>(cacheKey);

    if (data is null)
    {
      data = (List<Transaction>)await _repository.ListAsync(new TransactionsByDateSpec(userId, startDate, endDate), cancellationToken);
      _memoryCache.Set(cacheKey, data);
    }

    return data;
  }
}

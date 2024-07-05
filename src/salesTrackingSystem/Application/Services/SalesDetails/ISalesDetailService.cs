using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SalesDetails;

public interface ISalesDetailService
{
    Task<SalesDetail?> GetAsync(
        Expression<Func<SalesDetail, bool>> predicate,
        Func<IQueryable<SalesDetail>, IIncludableQueryable<SalesDetail, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<SalesDetail>?> GetListAsync(
        Expression<Func<SalesDetail, bool>>? predicate = null,
        Func<IQueryable<SalesDetail>, IOrderedQueryable<SalesDetail>>? orderBy = null,
        Func<IQueryable<SalesDetail>, IIncludableQueryable<SalesDetail, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<SalesDetail> AddAsync(SalesDetail salesDetail);
    Task<SalesDetail> UpdateAsync(SalesDetail salesDetail);
    Task<SalesDetail> DeleteAsync(SalesDetail salesDetail, bool permanent = false);
}

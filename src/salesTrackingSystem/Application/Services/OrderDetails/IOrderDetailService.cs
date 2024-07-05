using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.OrderDetails;

public interface IOrderDetailService
{
    Task<OrderDetail?> GetAsync(
        Expression<Func<OrderDetail, bool>> predicate,
        Func<IQueryable<OrderDetail>, IIncludableQueryable<OrderDetail, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<OrderDetail>?> GetListAsync(
        Expression<Func<OrderDetail, bool>>? predicate = null,
        Func<IQueryable<OrderDetail>, IOrderedQueryable<OrderDetail>>? orderBy = null,
        Func<IQueryable<OrderDetail>, IIncludableQueryable<OrderDetail, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<OrderDetail> AddAsync(OrderDetail orderDetail);
    Task<OrderDetail> UpdateAsync(OrderDetail orderDetail);
    Task<OrderDetail> DeleteAsync(OrderDetail orderDetail, bool permanent = false);
}

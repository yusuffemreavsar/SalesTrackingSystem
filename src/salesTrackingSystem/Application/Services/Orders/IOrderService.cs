using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Orders;

public interface IOrderService
{
    Task<Order?> GetAsync(
        Expression<Func<Order, bool>> predicate,
        Func<IQueryable<Order>, IIncludableQueryable<Order, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Order>?> GetListAsync(
        Expression<Func<Order, bool>>? predicate = null,
        Func<IQueryable<Order>, IOrderedQueryable<Order>>? orderBy = null,
        Func<IQueryable<Order>, IIncludableQueryable<Order, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Order> AddAsync(Order order);
    Task<Order> UpdateAsync(Order order);
    Task<Order> DeleteAsync(Order order, bool permanent = false);
}

using Application.Features.Orders.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Orders;

public class OrderManager : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly OrderBusinessRules _orderBusinessRules;

    public OrderManager(IOrderRepository orderRepository, OrderBusinessRules orderBusinessRules)
    {
        _orderRepository = orderRepository;
        _orderBusinessRules = orderBusinessRules;
    }

    public async Task<Order?> GetAsync(
        Expression<Func<Order, bool>> predicate,
        Func<IQueryable<Order>, IIncludableQueryable<Order, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Order? order = await _orderRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return order;
    }

    public async Task<IPaginate<Order>?> GetListAsync(
        Expression<Func<Order, bool>>? predicate = null,
        Func<IQueryable<Order>, IOrderedQueryable<Order>>? orderBy = null,
        Func<IQueryable<Order>, IIncludableQueryable<Order, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Order> orderList = await _orderRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return orderList;
    }

    public async Task<Order> AddAsync(Order order)
    {
        Order addedOrder = await _orderRepository.AddAsync(order);

        return addedOrder;
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        Order updatedOrder = await _orderRepository.UpdateAsync(order);

        return updatedOrder;
    }

    public async Task<Order> DeleteAsync(Order order, bool permanent = false)
    {
        Order deletedOrder = await _orderRepository.DeleteAsync(order);

        return deletedOrder;
    }
}
